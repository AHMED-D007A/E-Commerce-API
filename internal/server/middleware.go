package server

import (
	"errors"
	"fmt"
	"log"
	"net/http"

	"github.com/AHMED-D007A/E-Commerce-API/internal/config"
	"github.com/golang-jwt/jwt"
)

func LogMW(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		fmt.Printf("%v %v %v", r.Method, r.URL, r.RemoteAddr)
		next.ServeHTTP(w, r)
	})
}

func AuthMW(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		authHeader := r.Header.Get("Authorization")

		if authHeader == "" {
			w.WriteHeader(http.StatusUnauthorized)
			w.Write([]byte(`{"error": "Authorization header missing"}`))
			return
		}

		// Check if the header starts with "Bearer "
		if len(authHeader) < 7 || authHeader[:7] != "Bearer " {
			w.WriteHeader(http.StatusUnauthorized)
			w.Write([]byte(`{"error": "Malformed token"}`))
			return
		}

		// Extract the token part from the Authorization header
		tokenStr := authHeader[7:]

		token, err := jwt.ParseWithClaims(tokenStr, jwt.StandardClaims{}, func(t *jwt.Token) (interface{}, error) {
			alg := t.Method.Alg()
			if alg != jwt.SigningMethodHS256.Name {
				w.WriteHeader(http.StatusUnauthorized)
				w.Write([]byte(`{"error" : "Invalid token signature}`))
				return nil, errors.New("invalid Signing Method")
			}

			return config.Envs.JWT_SECRET, nil
		})

		if err != nil {
			if err == jwt.ErrSignatureInvalid {
				w.WriteHeader(http.StatusUnauthorized)
				log.Print(err.Error())
				w.Write([]byte(`{"error" : "invalid signature"}`))
				return
			}
			w.WriteHeader(http.StatusUnauthorized)
			log.Print(err.Error())
			w.Write([]byte(`{"error" : "invalid token"}`))
			return
		}

		if !token.Valid {
			w.WriteHeader(http.StatusUnauthorized)
			w.Write([]byte(`{"error" : "invalid token"}`))
			return
		}

		next.ServeHTTP(w, r)
	})
}
