package server

import (
	"database/sql"
	"fmt"
	"net/http"

	"github.com/gorilla/mux"
)

type APIServer struct {
	addr string
	db   *sql.DB
}

func New(addr string, db *sql.DB) *APIServer {
	return &APIServer{
		addr: addr,
		db:   db,
	}
}

func (s *APIServer) Start() error {
	router := mux.NewRouter()
	router.Use(LogMW)

	authenRouter := router.PathPrefix("/api/v1").Subrouter()
	registerAuthenticationRoutes(authenRouter, s.db)

	fmt.Println("Server is running on port", s.addr)
	return http.ListenAndServe(s.addr, router)
}
