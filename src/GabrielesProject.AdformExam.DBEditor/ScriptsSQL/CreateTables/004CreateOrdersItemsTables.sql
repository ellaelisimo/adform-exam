CREATE TABLE IF NOT EXISTS orders_items
(
	id SERIAL PRIMARY KEY,
	item_id INT,
	order_id INT,
	FOREIGN KEY (item_id) REFERENCES items (id),
	FOREIGN KEY (order_id) REFERENCES orders (id)
)
