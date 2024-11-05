package auth

import (
	"time"

	"github.com/AHMED-D007A/E-Commerce-API/internal/config"
	"github.com/golang-jwt/jwt/v5"
)

var secretKey = config.Envs.JWT_SECRET

func createToken(claim string) (string, error) {
	claims := jwt.NewWithClaims(jwt.SigningMethodHS256, jwt.MapClaims{
		"sub": claim,
		"iss": "customer_auth",
		"aud": "customer",
		"iat": time.Now().Unix(),
		"exp": time.Now().Add(time.Hour * 24).Unix(),
	})

	token, err := claims.SignedString(secretKey)

	return token, err
}
