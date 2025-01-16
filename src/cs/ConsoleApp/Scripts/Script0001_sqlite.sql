create table Accounts
(
    Id INTEGER not null constraint PK_Accounts primary key autoincrement,
    Name       TEXT              not null,
    IsSelected INTEGER default 0 not null
);