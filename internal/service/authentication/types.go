package auth

type User struct {
	ID       string `json:"id"`
	Email    string `json:"email"`
	Name     string `json:"username"`
	Password string `json:"password"`
}

type UserPayload struct {
	Email    string `json:"email"`
	Name     string `json:"username"`
	Password string `json:"password"`
}
