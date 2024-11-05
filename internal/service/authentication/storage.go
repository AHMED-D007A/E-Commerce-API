package auth

import "database/sql"

type AuthStorage struct {
	db *sql.DB
}

func NewAuthStorage(db *sql.DB) *AuthStorage {
	return &AuthStorage{db: db}
}

func (s *AuthStorage) RegisterNewUser(user UserPayload) error {
	query := "INSERT INTO users(username, email, password_hash) VALUES($1, $2, $3)"
	_, err := s.db.Exec(query, user.Name, user.Email, user.Password)
	if err != nil {
		return err
	}

	return nil
}

func (s *AuthStorage) GetUser(email string) (UserPayload, error) {
	var user UserPayload
	query := "SELECT username, email, password_hash FROM users WHERE email=$1"
	err := s.db.QueryRow(query, email).Scan(&user.Name, &user.Email, &user.Password)
	if err != nil {
		return user, err
	}

	return user, nil
}
