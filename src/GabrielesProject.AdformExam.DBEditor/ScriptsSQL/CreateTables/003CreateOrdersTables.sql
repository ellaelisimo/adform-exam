CREATE TABLE IF NOT EXISTS orders
(
	id SERIAL PRIMARY KEY,
	status VARCHAR(100) NOT NULL,
	user_id INT,
	item_id INT,
	created_at TIMESTAMP,
	FOREIGN KEY (item_id) REFERENCES items (id)
)
