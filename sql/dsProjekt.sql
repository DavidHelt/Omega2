CREATE TABLE users (
  id INTEGER PRIMARY KEY,
  username nvarchar(50) UNIQUE,
  password TEXT
);

CREATE TABLE messages (
  id INTEGER PRIMARY KEY,
  sender_id INTEGER,
  subject TEXT,
  body TEXT,
  sent_at datetime DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY(sender_id) REFERENCES users(id)
);

CREATE TABLE message_recipients (
  message_id INTEGER,
  recipient_id INTEGER,
  PRIMARY KEY(message_id, recipient_id),
  FOREIGN KEY(message_id) REFERENCES messages(id),
  FOREIGN KEY(recipient_id) REFERENCES users(id)
);



