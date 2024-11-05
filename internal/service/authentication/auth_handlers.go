package auth

import (
	"encoding/json"
	"log"
	"net/http"

	"golang.org/x/crypto/bcrypt"
)

type AuthHandler struct {
	storage *AuthStorage
}

func NewAuthHandler(storage *AuthStorage) *AuthHandler {
	return &AuthHandler{storage: storage}
}

func (h *AuthHandler) RegisterUser(w http.ResponseWriter, r *http.Request) {
	var userPayload UserPayload
	err := json.NewDecoder(r.Body).Decode(&userPayload)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		log.Print(err.Error())
		log.Println("Error decoding user payload")
		return
	}
	password, err := bcrypt.GenerateFromPassword([]byte(userPayload.Password), bcrypt.DefaultCost)
	userPayload.Password = string(password)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		log.Print(err.Error())
		return
	}
	err = h.storage.RegisterNewUser(userPayload)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		log.Print(err.Error())
		return
	}
	token, err := createToken(userPayload.Email)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		log.Print(err.Error())
		return
	}
	response := map[string]string{"token": token}
	w.WriteHeader(http.StatusCreated)
	err = json.NewEncoder(w).Encode(response)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		log.Print(err.Error())
		return
	}
}

func (h *AuthHandler) LoginUser(w http.ResponseWriter, r *http.Request) {
	var userPayload UserPayload
	err := json.NewDecoder(r.Body).Decode(&userPayload)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		log.Print(err.Error())
		log.Println("Error decoding user payload")
		return
	}
	user, err := h.storage.GetUser(userPayload.Email)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		log.Print(err.Error())
		return
	}
	err = bcrypt.CompareHashAndPassword([]byte(user.Password), []byte(userPayload.Password))
	if err != nil {
		http.Error(w, err.Error(), http.StatusUnauthorized)
		log.Print(err.Error())
		return
	}
	token, err := createToken(user.Email)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		log.Print(err.Error())
		return
	}
	response := map[string]string{"token": token}
	w.WriteHeader(http.StatusOK)
	err = json.NewEncoder(w).Encode(response)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		log.Print(err.Error())
		return
	}
}
