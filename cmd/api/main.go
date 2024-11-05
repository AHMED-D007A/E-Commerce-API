package main

import (
	"fmt"

	"github.com/AHMED-D007A/E-Commerce-API/internal/config"
	"github.com/AHMED-D007A/E-Commerce-API/internal/server"
)

func main() {
	connStr := fmt.Sprintf("host=%v port=%v user=%v password=%v dbname=%v sslmode=disable",
		config.Envs.DB_HOST, config.Envs.DB_PORT, config.Envs.DB_USERNAME,
		config.Envs.DB_PASSWORD, config.Envs.DB_NAME)
	db := server.NewDB(connStr)
	defer db.Close()

	server := server.New(":4000", db)
	err := server.Start()
	if err != nil {
		panic(err)
	}
}
