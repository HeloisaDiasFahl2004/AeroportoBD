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

CREATE TABLE Restrito(
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

CREATE TABLE Bloqueado(
	CNPJ varchar(14) not null

	CONSTRAINT PK_CNPJ_Bloqueado PRIMARY KEY(CNPJ)
);

CREATE TABLE Aeroporto(
	IATA varchar(3)

	CONSTRAINT PK_IATA_Aeroporto PRIMARY KEY (IATA)
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
	IDPASSAGEM int  not null, -- PA 0000
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


insert into Aeroporto (Iata) values ('BSB'),('CGH'),('GIG'),('SSA'),('FLN'),('POA'),('VCP'),('REC'),('CWB'),('BEL'),('VIX'),('SDU'),('CGB'),('CGR'),('FOR'),('MCP'),('MGF'),('GYN'),('NVT'),('MAO'),('NAT'),('BPS'),('MCZ'),('PMW'),('SLZ'),('GRU'),('LDB'),('PVH'),('RBR'),('JOI'),('UDI'),('CXJ'),('IGU'),('THE'),('AJU'),('JPA'),('PNZ'),('CNF'),('BVB'),('CPV'),('STM'),('IOS'),('JDO'),('IMP'),('XAP'),('MAB'),('CZS'),('PPB'),('CFB'),('FEN'),('JTC'),('MOC');
select * from Aeroporto;

CREATE PROCEDURE CadastroPassagens (@valor float)
AS 
    BEGIN
	declare 
	@idPassagem int,
	@idVoo varchar(5),
	@count int = 0,
	@situacao char= 'L', 
	@qtd int,
	@dataUltimaOperacao DateTime = GetDate()

	SELECT @idVoo  = MAX(IdVoo) FROM dbo.Voo
	SELECT @idPassagem = MAX(idPassagem) FROM dbo.Passagem
	SELECT @qtd = capacidade FROM aeronave, voo WHERE   iDVOO = @idVoo
	SELECT @idPassagem = ISNULL(@idPassagem,1)
	
	WHILE @count <= @qtd
		BEGIN
	        INSERT INTO dbo.passagem (idPassagem, idVoo, ValorUnitario, situacao, DataUltimaOperacao) VALUES(@idPassagem, @idVoo, @valor, @situacao, @dataUltimaOperacao)
			SET @count = @count + 1
			SET @idPassagem = @idPassagem + 1
		END
END


/* Ver se a tabela está sendo montada certo

select * from CompanhiaAerea 
select * from  Passageiro
select *  from Aeronave
select * from VendaPassagem
select * from Venda
select * from Passagem
select * from Voo

select * from Aeroporto
select * from Bloqueado
select * from Restrito

*/
