﻿CREATE TABLE IF NOT EXISTS orders
(
	id SERIAL PRIMARY KEY,
	status VARCHAR(100) NOT NULL,
	user_id INT,
	created_at TIMESTAMP
)
