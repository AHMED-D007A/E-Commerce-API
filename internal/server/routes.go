package server

import "github.com/gorilla/mux"

func registerAuthenticationRoutes(router *mux.Router) {
	router.HandleFunc("/login", nil)
	router.HandleFunc("/register", nil)
}
