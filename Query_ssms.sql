CREATE TABLE clients (
  id INT NOT NULL PRIMARY KEY IDENTITY,
  name VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL UNIQUE,
  phone VARCHAR(20) NULL,
  address VARCHAR(255) NULL,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO clients (name, email, phone, address) VALUES
('Bill Gates','bill.gates@microsoft.com','+123456789','New York, USA'),
('Elon Musk','elon.musk@spacex.com','+111222333','Florida, USA'),
('Will Smith','will.smith@gmail.com','+111333555','California, USA'),
('Jeff Bezos','jeff.bezos@amazon.com','+333111444','Texas, USA'),
('Mark Zuckerberg','mark.zuckerberg','+444777855','London, England'),
('Warren Buffet','warren.buffet@stock.com','+555666777','Paris, France');

select * from clients;
