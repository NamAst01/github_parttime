/*DROP DATABASE part_time_job_database;  */
/*CREATE DATABASE part_time_job_database; */
USE part_time_job_database ;
CREATE TABLE [vote] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [rated_user_id] INT NOT NULL,
  [reviewer_id] INT NOT NULL,
  [number_star] SMALLINT NOT NULL
)
GO

CREATE TABLE [comment] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [applicant_id] INT NOT NULL,
  [employer_id] INT NOT NULL,
  [content] NVARCHAR(1000) NOT NULL
)
GO

CREATE TABLE [job_history] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [applicant_id] INT NOT NULL,
  [title] NVARCHAR(1000) NOT NULL,
  [company] VARCHAR(255) NOT NULL,
  [description] NVARCHAR(1000) NOT NULL
)
GO

CREATE TABLE [admin] (
  [admin_id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [full_name] NVARCHAR(1000) NOT NULL,
  [email] VARCHAR(255) NOT NULL,
  [password] VARCHAR(255) NOT NULL,
  [level] SMALLINT NOT NULL,
  [status] SMALLINT NOT NULL
)
GO

CREATE TABLE [job_detail] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [title] NVARCHAR(1000) NOT NULL,
  [category_id] INT NOT NULL,
  [company] NVARCHAR(1000) NOT NULL,
  [description] NVARCHAR(1000) NOT NULL,
  [salary] INT NOT NULL,
  [location] NVARCHAR(1000) NOT NULL,
  [deadline] DATETIME NOT NULL,
  [created_at] DATETIME NOT NULL,
  [status] SMALLINT NOT NULL,
  [employer_id] INT NOT NULL
)
GO

CREATE TABLE [employer] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [full_name] NVARCHAR(1000) NOT NULL,
  [email] VARCHAR(255) NOT NULL,
  [password] VARCHAR(255) NOT NULL,
  [phone] VARCHAR(255) NOT NULL,
  [company] NVARCHAR(1000) NOT NULL,
  [address] NVARCHAR(1000) NOT NULL,
  [position] NVARCHAR(1000) NOT NULL,
  [image] VARCHAR NOT NULL,
  [created_at] DATETIME NOT NULL,
  [last_login_at] DATETIME NOT NULL,  
  [rating_id] INT NOT NULL,
  [status] SMALLINT NOT NULL
)
GO

CREATE TABLE [job_applicant] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [full_name] NVARCHAR(1000) NOT NULL,
  [email] VARCHAR(255) NOT NULL,
  [password] VARCHAR(255) NOT NULL,
  [phone] VARCHAR(255) NOT NULL,
  [dob] DATETIME NOT NULL,
  [image] VARCHAR NOT NULL,
  [created_at] DATETIME NOT NULL,
  [last_login_at] DATETIME NOT NULL,
  [rating_id] INT NOT NULL,
  [status] SMALLINT NOT NULL
)
GO

CREATE TABLE [category] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [name] NVARCHAR(1000) NOT NULL
)
GO

CREATE TABLE [rating] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [quantity] INT NOT NULL,
  [scale] FLOAT(2) 
)
GO

CREATE TABLE [job_application] (
  [id] INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [applicant_id] INT NOT NULL,
  [job_id] INT NOT NULL,
  [created_at] DATETIME NOT NULL,
  [status] SMALLINT NOT NULL
)
GO

ALTER TABLE [job_detail] ADD FOREIGN KEY ([employer_id]) REFERENCES [employer] ([id])
GO

ALTER TABLE [comment] ADD FOREIGN KEY ([applicant_id]) REFERENCES [job_applicant] ([id])
GO

ALTER TABLE [comment] ADD FOREIGN KEY ([employer_id]) REFERENCES [employer] ([id])
GO

ALTER TABLE [job_application] ADD FOREIGN KEY ([job_id]) REFERENCES [job_detail] ([id])
GO

ALTER TABLE [job_applicant] ADD FOREIGN KEY ([rating_id]) REFERENCES [rating] ([id])
GO

ALTER TABLE [job_history] ADD FOREIGN KEY ([applicant_id]) REFERENCES [job_applicant] ([id])
GO

ALTER TABLE [job_detail] ADD FOREIGN KEY ([category_id]) REFERENCES [category] ([id])
GO

ALTER TABLE [employer] ADD FOREIGN KEY ([rating_id]) REFERENCES [rating] ([id])
GO

ALTER TABLE [job_application] ADD FOREIGN KEY ([applicant_id]) REFERENCES [job_applicant] ([id])
GO
-- Insert data into [category] table
INSERT INTO [category] ([name]) VALUES
  ('Category 1'),
  ('Category 2'),
  ('Category 3'),
  ('Category 4'),
  ('Category 5');
GO
-- Insert data into [rating] table
INSERT INTO [rating] ([quantity], [scale]) VALUES
  (100, 4.5),
  (150, 4.0),
  (80, 3.5),
  (200, 5.0),
  (120, 4.2);
GO
-- Insert data into [job_detail] table
INSERT INTO [job_detail] ([title], [category_id], [company], [description], [salary], [location], [deadline], [created_at], [status], [employer_id]) VALUES
  ('Job Title 1', 1, 'Company A', 'Job Description 1', 2500, 'Location 1', '2023-10-15', GETDATE(), 1, 1),
  ('Job Title 2', 2, 'Company B', 'Job Description 2', 2800, 'Location 2', '2023-10-20', GETDATE(), 1, 2),
  ('Job Title 3', 3, 'Company C', 'Job Description 3', 3000, 'Location 3', '2023-10-25', GETDATE(), 1, 3),
  ('Job Title 4', 4, 'Company D', 'Job Description 4', 3200, 'Location 4', '2023-10-30', GETDATE(), 1, 4),
  ('Job Title 5', 5, 'Company E', 'Job Description 5', 3500, 'Location 5', '2023-11-05', GETDATE(), 1, 5);
GO
-- Insert data into [job_applicant] table
INSERT INTO [job_applicant] ([full_name], [email], [password], [phone], [dob], [image], [created_at], [last_login_at], [rating_id], [status]) VALUES
  ('Applicant 1', 'applicant1@example.com', 'password1', '123-456-7890', '1990-01-15', 'image1.jpg', GETDATE(), GETDATE(), 1, 1),
  ('Applicant 2', 'applicant2@example.com', 'password2', '987-654-3210', '1995-03-20', 'image2.jpg', GETDATE(), GETDATE(), 2, 1),
  ('Applicant 3', 'applicant3@example.com', 'password3', '555-555-5555', '1988-08-10', 'image3.jpg', GETDATE(), GETDATE(), 3, 1),
  ('Applicant 4', 'applicant4@example.com', 'password4', '777-888-9999', '1992-11-05', 'image4.jpg', GETDATE(), GETDATE(), 4, 1),
  ('Applicant 5', 'applicant5@example.com', 'password5', '111-222-3333', '1997-05-25', 'image5.jpg', GETDATE(), GETDATE(), 5, 1);
GO
-- Insert data into [employer] table
INSERT INTO [employer] ([full_name], [email], [password], [phone], [company], [address], [position], [image], [created_at], [last_login_at], [rating_id], [status]) VALUES
  ('Employer 1', 'employer1@example.com', 'password1', '111-111-1111', 'Company X', 'Address 1', 'CEO', 'employer_image1.jpg', GETDATE(), GETDATE(), 1, 1),
  ('Employer 2', 'employer2@example.com', 'password2', '222-222-2222', 'Company Y', 'Address 2', 'Manager', 'employer_image2.jpg', GETDATE(), GETDATE(), 2, 1),
  ('Employer 3', 'employer3@example.com', 'password3', '333-333-3333', 'Company Z', 'Address 3', 'HR Manager', 'employer_image3.jpg', GETDATE(), GETDATE(), 3, 1),
  ('Employer 4', 'employer4@example.com', 'password4', '444-444-4444', 'Company W', 'Address 4', 'Director', 'employer_image4.jpg', GETDATE(), GETDATE(), 4, 1),
  ('Employer 5', 'employer5@example.com', 'password5', '555-555-5555', 'Company V', 'Address 5', 'Manager', 'employer_image5.jpg', GETDATE(), GETDATE(), 5, 1);
GO
-- Insert data into [job_application] table
INSERT INTO [job_application] ([applicant_id], [job_id], [created_at], [status]) VALUES
  (1, 1, GETDATE(), 1),
  (2, 2, GETDATE(), 1),
  (3, 3, GETDATE(), 1),
  (4, 4, GETDATE(), 1),
  (5, 5, GETDATE(), 1);
