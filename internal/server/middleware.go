package server

import (
	"fmt"
	"net/http"
)

func LogMW(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		fmt.Printf("%v %v %v", r.Method, r.URL, r.RemoteAddr)
		next.ServeHTTP(w, r)
	})
}
