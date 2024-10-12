--а) наполнить базу тестовыми данными пользуясь оператором Insert;
create extension if not exists "uuid-ossp";

INSERT INTO currencies (Id, Code, Name, Symbol) VALUES
    (uuid_generate_v4(), 'USD', 'US Dollar', '$'),
    (uuid_generate_v4(), 'EUR', 'Euro', '€'),
    (uuid_generate_v4(), 'RUB', 'Russian Ruble', '₽');

INSERT INTO clients (Id, FirstName, LastName, MiddleName, BirthDay, PhoneNumber, PassportNumber) VALUES
    (uuid_generate_v4(), 'Иван', 'Иванов', 'Иванович', '1980-05-15', '+79161234567', '1234567890'),
    (uuid_generate_v4(), 'Петр', 'Петров', 'Петрович', '1992-08-20', '+79169876543', '0987654321'),
    (uuid_generate_v4(), 'Сергей', 'Сергеев', 'Сергеевич', '1975-12-10', '+79001234567', '1122334455'),
    (uuid_generate_v4(), 'Алексей', 'Алексеев', 'Алексеевич', '1995-03-05', '+79011223344', '2233445566');

INSERT INTO employees (Id, FirstName, LastName, MiddleName, BirthDay, PhoneNumber, PassportNumber, Salary, Contract) VALUES
    (uuid_generate_v4(), 'Ольга', 'Сидорова', 'Петровна', '1985-02-12', '+79876543210', '3344556677', 50000, 'Контракт 1'),
    (uuid_generate_v4(), 'Анна', 'Николаева', 'Александровна', '1990-06-18', '+79781234567', '5566778899', 60000, 'Контракт 2'),
    (uuid_generate_v4(), 'Виктор', 'Кузнецов', 'Викторович', '1978-11-22', '+79003456789', '7788990011', 70000, 'Контракт 3');

INSERT INTO accounts (Id, Amount, CurrencyId) VALUES
    (uuid_generate_v4(), 10, (SELECT Id FROM currencies WHERE Code = 'USD')),
    (uuid_generate_v4(), 1000, (SELECT Id FROM currencies WHERE Code = 'EUR')),
    (uuid_generate_v4(), 0, (SELECT Id FROM currencies WHERE Code = 'RUB'));
    
INSERT INTO clientsofaccounts (ClientId, AccountId) VALUES
    ((SELECT Id FROM clients WHERE FirstName = 'Иван' AND LastName = 'Иванов'), (SELECT Id FROM accounts WHERE CurrencyId = (SELECT Id FROM currencies WHERE Code = 'USD'))),
    ((SELECT Id FROM clients WHERE FirstName = 'Петр' AND LastName = 'Петров'), (SELECT Id FROM accounts WHERE CurrencyId = (SELECT Id FROM currencies WHERE Code = 'EUR'))),
    ((SELECT Id FROM clients WHERE FirstName = 'Сергей' AND LastName = 'Сергеев'), (SELECT Id FROM accounts WHERE CurrencyId = (SELECT Id FROM currencies WHERE Code = 'RUB'))),
    ((SELECT Id FROM clients WHERE FirstName = 'Алексей' AND LastName = 'Алексеев'), (SELECT Id FROM accounts WHERE CurrencyId = (SELECT Id FROM currencies WHERE Code = 'USD'))),
    ((SELECT Id FROM clients WHERE FirstName = 'Иван' AND LastName = 'Иванов'), (SELECT Id FROM accounts WHERE CurrencyId = (SELECT Id FROM currencies WHERE Code = 'EUR'))),
    ((SELECT Id FROM clients WHERE FirstName = 'Петр' AND LastName = 'Петров'), (SELECT Id FROM accounts WHERE CurrencyId = (SELECT Id FROM currencies WHERE Code = 'RUB')));

--б) провести выборки клиентов, у которых сумма на счету ниже определенного значения, отсортированных в порядке возрастания суммы
select c.FirstName, c.LastName, a.Amount
from clients c 
join clientsofaccounts ca on c.id = ca.clientid
join accounts a on ca.accountid = a.Id
where a.Amount < 1000 
order by a.Amount;

--в) провести поиск клиента с минимальной суммой на счете;
select c.FirstName, c.LastName, a.Amount
from clients c 
join clientsofaccounts ca on c.id = ca.clientid
join accounts a on ca.accountid = a.Id
order by a.Amount
limit 1;

--г) провести подсчет суммы денег у всех клиентов банка
select SUM(a.Amount) as TotalBankFunds
from accounts a;

--д) с помощью оператора Join, получить выборку банковских счетов и их владельцев
select c.FirstName, c.LastName, a.Amount, cur.Code AS Currency
from clients c
join clientsOfaccounts ca on c.Id = ca.ClientId
join accounts a on ca.AccountId = a.Id
join currencies cur on a.CurrencyId = cur.Id;

--е) вывести клиентов от самых старших к самым младшим;
select FirstName, LastName, BirthDay
from  clients
order by BirthDay;

--ж) подсчитать количество человек, у которых одинаковый возраст;
select 
extract(year from AGE(NOW(), BirthDay)) as Age,
COUNT(*) as CountOfPeople
from clients
group by Age
order by Age;

--з) сгруппировать клиентов банка по возрасту
select 
extract(year from AGE(NOW(), BirthDay)) as Age
from 
clients
order by 
Age;

--и) вывести только N человек из таблицы
select FirstName, LastName, BirthDay
from clients
limit 10;