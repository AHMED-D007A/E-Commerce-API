-- Drop the trigger
DROP TRIGGER IF EXISTS update_users_updated_at ON users;

-- Drop the function
DROP FUNCTION IF EXISTS update_updated_at_column;

-- Drop the users table
DROP TABLE IF EXISTS users;