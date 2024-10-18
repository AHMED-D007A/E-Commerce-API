package main

import "github.com/AHMED-D007A/E-Commerce-API/internal/server"

func main() {
	server := server.New(":4000", nil)
	err := server.Start()
	if err != nil {
		panic(err)
	}
}
