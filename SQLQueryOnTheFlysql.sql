CREATE DATABASE OnTheFly
USE OnTheFly

CREATE TABLE Passageiro(
	
	CPF varchar(11) not null,
	Nome varchar (50) not null,
	DataNascimento Date not null,
	Sexo char(1) not null,
	DataUltimaCompra date not null,
	DataCadastro date not null,
	Situacao char(1) not null -- A/I
	
	CONSTRAINT PK_CPF_Passageiro PRIMARY KEY (CPF)
);

CREATE TABLE Restritos(
	CPF varchar(11) not null

	CONSTRAINT PK_CPF_Restrito PRIMARY KEY (CPF)
);

CREATE TABLE CompanhiaAerea(
	CNPJ varchar(14) not null,
	RazaoSocial varchar(50) not null,
	DataAbertura date not null,
	DataUltimoVoo date not null,
	DataCadastro date not null,
    Situacao char(1) not null, -- A/I
	CONSTRAINT PK_CNPJ_CompanhiaAerea PRIMARY KEY(CNPJ)
);

CREATE TABLE Bloqueados(
	CNPJ varchar(14) not null

	CONSTRAINT PK_CNPJ_Bloqueado PRIMARY KEY(CNPJ)
);

CREATE TABLE Aeoroportos(
	IATA varchar(3)

	CONSTRAINT PK_IATA_Aeroportos PRIMARY KEY (IATA)
);

CREATE TABLE Aeronave(
	INSCRICAO varchar(6) not null, -- PT-MXE
	Capacidade int not null,
	UltimaVenda date not null,
	DataCadastro date not null,
	Situacao char(1),
	CNPJ varchar(14) not null ,
	
	CONSTRAINT PK_INSCRICAO_Aeronave PRIMARY KEY (INSCRICAO),
	FOREIGN KEY (CNPJ) references CompanhiaAerea(CNPJ),
	
);

CREATE TABLE Voo(
	IDVOO varchar (5) not null, --V0000
	IATA varchar(3) not null, --Destino
	DataVoo date not null,
	DataCadastro date not null,
	QuantidadeAssentosOcupados int not null,
	Situacao char(1) not null,
	Constraint PK_IDVOO_Voo PRIMARY KEY (IDVOO)
);

CREATE TABLE Passagem(
	IDPASSAGEM varchar(6) not null, --PA0000
	IDVOO varchar (5) not null, --V0000
	
	DataUltimaOperacao date not null,
	ValorUnitario float not null,
	Situacao char(1) not null,
	
	FOREIGN KEY (IDVOO) references Voo(IDVOO),
	CONSTRAINT PK_IDPASSAGEM_PASSAGEM PRIMARY KEY (IDPASSAGEM),
  
);

CREATE TABLE Venda(
	IDVENDA varchar(5) not null,
	DataVenda date not null,
	ValorTotal float not null,
	CPF varchar(11) not null,

	CONSTRAINT PK_IDVENDA_VENDA PRIMARY KEY (IDVENDA),
	FOREIGN KEY (CPF) references Passageiro(CPF),
);

CREATE TABLE VendaPassagem(--ITEM VENDA
	IDITEMVENDA varchar(5) not null,
	IDVENDA varchar(5) not null,
	ValorUnitario float not null
	FOREIGN KEY (IDVENDA) references Venda(IDVENDA),
);



