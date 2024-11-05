package auth

import "net/http"

type AuthHandler struct {
	storage *AuthStorage
}

func NewAuthHandler(storage *AuthStorage) *AuthHandler {
	return &AuthHandler{storage: storage}
}

func (h *AuthHandler) RegisterUser(w http.ResponseWriter, r *http.Request) {
	// Implement the logic to register a user
}

func (h *AuthHandler) LoginUser(w http.ResponseWriter, r *http.Request) {
	// Implement the logic to login a user
}
