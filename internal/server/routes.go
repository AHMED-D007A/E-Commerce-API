package server

import (
	"database/sql"

	auth "github.com/AHMED-D007A/E-Commerce-API/internal/service/authentication"
	"github.com/gorilla/mux"
)

func registerAuthenticationRoutes(router *mux.Router, db *sql.DB) {
	authHandler := auth.NewAuthHandler(auth.NewAuthStorage(db))
	router.HandleFunc("/login", authHandler.LoginUser)
	router.HandleFunc("/register", authHandler.RegisterUser)
}
