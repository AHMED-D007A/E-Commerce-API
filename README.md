# E-Commerce-API
- Simple API for an e-commerce platform. 
- This project is from the project based road map, you can find it [here](https://roadmap.sh/backend/projects).
- You can read more about the project form [here](https://roadmap.sh/projects/ecommerce-api).

### Configuration
- Create a `.env` file in the root directory and add the following configurations.
- You can find the sample `.env` file [here](.env).
- You can access these variables in the `.env` file with the config package.
- The configuration file can be found at `internal/config/config.go`.

### Authentication Service
- The authentication service is implemented using JWT.
- You can find the authentication service [here](internal/service/authentication).
- The authentication is done in every request using the middleware.
- The JWT token is genereated for two endpoints, `/login` and `/register`, their handlers can be found [here](internal/service/authentication/auth_handlers.go).
- Accessing the database and querying for users info is done in the authentication service folder [here](internal/service/authentication/storage.go.go).
- The request and response json types are defined in [here](internal/service/authentication/types.go).
```
POST /register
{
  "email": "user1@email"
  "username": "user1",
  "password": "password1"
}
```
```
POST /login
{
  "email": "user1@email"
  "password": "password1"
}
```