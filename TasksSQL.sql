--a
select c.FirstName, c.LastName, a.Amount
from clients c 
join clientsofaccounts ca on c.id = ca.clientid
join accounts a on ca.accountid = a.Id
where a.Amount < 1000 
order by a.Amount;

--b
select c.FirstName, c.LastName, a.Amount
from clients c 
join clientsofaccounts ca on c.id = ca.clientid
join accounts a on ca.accountid = a.Id
order by a.Amount
limit 1;

--c
select SUM(a.Amount) as TotalBankFunds
from accounts a;

--d
select c.FirstName, c.LastName, a.Amount, cur.Code AS Currency
from clients c
join clientsOfaccounts ca on c.Id = ca.ClientId
join accounts a on ca.AccountId = a.Id
join currencies cur on a.CurrencyId = cur.Id;

--e
select FirstName, LastName, BirthDay
from  clients
order by BirthDay;

--f
select 
extract(year from AGE(NOW(), BirthDay)) as Age,
COUNT(*) as CountOfPeople
from clients
group by Age
order by Age;

--g
select 
extract(year from AGE(NOW(), BirthDay)) as Age
from 
clients
order by 
Age;

--h
select FirstName, LastName, BirthDay
from clients
limit 10;