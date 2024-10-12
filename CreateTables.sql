create table currencies
(
	Id uuid primary key,
	Code varchar not null,
	Name varchar not null,
	Symbol varchar not null
);

create table accounts
(
	Id uuid primary key,
	Amount decimal not null,
	CurrencyId uuid references currencies(Id)
);

create table clients
(
	Id uuid primary key, 
    FirstName varchar not null,
    LastName varchar not null,
    MiddleName varchar,
    BirthDay date not null,
    PhoneNumber varchar,
    PassportNumber VARCHAR unique  
);

create table clientsOfaccounts
(
    ClientId uuid references clients(Id),
    AccountId uuid references accounts(Id),
    primary key(ClientId, AccountId)
);

create table employees
(
	Id uuid primary key, 
    FirstName varchar not null,
    LastName varchar not null,
    MiddleName varchar,
    BirthDay date not null,
    PhoneNumber varchar not null,
    PassportNumber VARCHAR unique,
    Salary int not null,
    Contract varchar not null
);