package server

import (
	"database/sql"
	"fmt"
	"log"
	"net/http"

	"github.com/gorilla/mux"
	_ "github.com/lib/pq"
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

func NewDB(connStr string) *sql.DB {
	db, err := sql.Open("postgres", connStr)
	if err != nil {
		log.Fatal(err.Error())
	}

	err = db.Ping()
	if err != nil {
		log.Fatal(err.Error())
	}

	return db
}

func (s *APIServer) Start() error {
	router := mux.NewRouter()
	router.Use(LogMW)

	authenRouter := router.PathPrefix("/api/v1").Subrouter()
	registerAuthenticationRoutes(authenRouter, s.db)

	fmt.Println("Server is running on port", s.addr)
	return http.ListenAndServe(s.addr, router)
}
