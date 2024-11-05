package auth

import "database/sql"

type AuthStorage struct {
	db *sql.DB
}

func NewAuthStorage(db *sql.DB) *AuthStorage {
	return &AuthStorage{db: db}
}
