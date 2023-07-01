Платформа: .NET 5 (хотел совместить с другим тестовым заданием)

с кратким описанием назначений файлов
краткой инструкцией по использованию программных модулей, 
указать в какой среде разработки сделан проект, указать какие из необязательных требований выполнены. 

1. Core:
1.1 Common
1.1.1 BaseEntity - абстрактный класс в качестве базового класса для всех сущностей базы данных
1.1.2 IGenericRepository - Generic интерфейс репозиторий для реализации паттерна репозиторий
1.1.3 IUnitOfWork - интерфейс для реализации паттерна UnitOfWork

1.2 DTOs
1.2.1 CoinsCollection - DTO для передачи данных о наборе монет в виде словаря с ключом в качестве номинала монеты и значения словаря в качестве количества монет с данным номиналом;
	CoinsCollectionExtensions - статичный класс методов расширений для работы со структурой CoinsCollection
1.2.2 Product - DTO для передачи данных о продуктах/напитков терминала
1.2.3 PurchaseResult - DTO для передачи данных о сдаче и купленного напитка в терминале
1.2.4 UpdateData - DTO для передачи данных о текущем состоянии терминала и его хранилища

1.3 Entities
1.3.1 CoinDenomination - сущность монет и информации связанной с ней
1.3.1 CoinOperation - сущность операций с монетами (тип операции, номинал монеты, ее количество)
1.3.1 StorageItem - сущность напитков или потенциально других товаров терминала

1.4 Handlers
1.4.1 FileHandler - предназначен для работы с файлами. Например, при получении файла для импорта напитков

1.5 Interfaces
1.5.1 Interfaces -> Handlers -> IFileHandler - интерфейс для хэндлера работы с файлами (FileHandler)
1.5.2 Interfaces -> Repositories -> ICoinDenominationRepository - интерфейс для дополнительной функциональности с сущностями монет
1.5.3 Interfaces -> Services -> IAdminTerminalService - интерфейс сервиса работы с функциональностью админов
1.5.3 Interfaces -> Services -> IStorageService - интерфейс сервиса работы со внутренним хранилищем терминала (напитки, монеты)
1.5.3 Interfaces -> Services -> ITerminalService - интерфейс сервиса работы с функциональностью терминала и его хранилища
1.5.3 Interfaces -> Services -> IUserTerminalService - интерфейс сервиса работы с функциональностью пользователей терминала

1.6 Services
1.6.1 AdminTerminalService - сервис для работы с запросами от админов
1.6.2 StorageService - сервис для работы со внутренним хранилищем терминала (напитки, монеты)
1.6.3 TerminalService - сервис для работы с функциональностью терминала и его хранилища
1.6.4 UserTerminalService - сервис для работы с функциональностью пользователей терминала

2. Infrastructure
2.1 Data
2.1.1 Data -> Config -> CoinDenominationConfiguration - конфигурация entity framework для сущности монет
2.1.2 Data -> Config -> CoinOperationConfiguration - конфигурация entity framework для сущности операций с монетами
2.1.3 Data -> Config -> StorageItemConfiguration - конфигурация entity framework для сущности предметов терминала (напитки и и.п)
2.1.4 BeverageVendingMachineContext - класс DbContext приложения, служит как мост между сущностями и базой данных
2.1.4 UnitOfWork - класс реализация паттерна UnitOfWork

2.2 Migrations - папка с миграциями entity framework

2.3 Repositories
2.3.1 CoinDenominationRepository - реализация интерфейса для дополнительной функциональности с сущностями монет
2.3.2 GenericRepository - реализация паттерна репозиторий 

2.4 Seed data
2.4.1 BeverageVendingMachineContextSeed - класс для того чтобы наполнить базу данных во время первого запуска
2.4.2 coinDenominations.json, coinOperations.json, storageItems.json - json данные для наполнения базы данных во время первого запуска

3. Web
3.1 wwwroot
3.2 API
3.3 App_Data
3.4 Controllers
3.5 Extensions
3.6 Views
