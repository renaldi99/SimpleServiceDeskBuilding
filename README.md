## Simple Service Desk Building

Simple rest api to create ticket service information building that system focuses on supporting users resolving issues in building.

### How to run ⚙️

Must have that required such as NET 6 SDK, PostgreSQL, and Visual Studio for text editor code. Run step by step for build database and table.

---
<b>Query Create Table:</b>
```
create database db_ticketbuilding

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

create table if not exists tb_master_user (
	id uuid default uuid_generate_v4() not null,
	fullname varchar(50) null,
	username varchar(50) null,
	password varchar(50) null,
	email varchar(50) null,
	identity_employee varchar(20) null,
	is_active varchar(10) null,
	role varchar(10) null,
	refresh_token varchar(200) null,
	constraint "PK_tb_master_user" primary key(id)
)

create table if not exists tb_master_ticket_building (
	id uuid default uuid_generate_v4() not null,
	id_user_created uuid default uuid_generate_v4() null,
	title varchar(100) null,
	description varchar(100) null,
	file_attachment varchar(100) null,
	constraint "PK_tb_master_ticket_building" primary key(id)
)

create table if not exists tb_master_activity_ticket_building (
	id uuid default uuid_generate_v4() not null,
	id_ticket uuid default uuid_generate_v4() null,
	id_user_done uuid default uuid_generate_v4() null,
	status varchar(20) null,
	description varchar(100) null,
	file_attachment varchar(100) null,
	last_created_at timestamp null,
	last_updated_at timestamp null,
	last_finished_at timestamp null,
	is_closed bool null,
	constraint "PK_tb_master_activity_ticket_building" primary key(id)
)
```
---
<b>Setting connection string property "dev" in appsettings.json:</b>
```
Host=localhost;Database={DATABASE HERE};Username={USERNAME HERE};Password={PASSWORD HERE};Port=5432;Integrated Security=true;Pooling=true
```

### Explain
---
```
Identity employee only has internal and vendor. Internal cannot take or process activity ticket, for activity ticket will taken by vendor to solve problem. And then this status only "create", "progress", "cancel", and "done" so beside that program will give an error, exception or something like that. Hopefully this program will enjoy to use.
```

### Next Update
---
- Set variable config with .env 🚀
- More Features 🚀

### More
---
> If have an issue when run this program, please give a feedback.