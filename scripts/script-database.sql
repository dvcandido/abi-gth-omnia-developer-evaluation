CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20241014011203_InitialMigrations') THEN
    CREATE TABLE "Users" (
        "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
        "Username" character varying(50) NOT NULL,
        "Password" character varying(100) NOT NULL,
        "Phone" character varying(20) NOT NULL,
        "Email" character varying(100) NOT NULL,
        "Status" character varying(20) NOT NULL,
        "Role" character varying(20) NOT NULL,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20241014011203_InitialMigrations') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20241014011203_InitialMigrations', '8.0.10');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250326204448_AlterUserTable') THEN
    ALTER TABLE "Users" ADD "CreatedAt" timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250326204448_AlterUserTable') THEN
    ALTER TABLE "Users" ADD "UpdatedAt" timestamp with time zone;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250326204448_AlterUserTable') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250326204448_AlterUserTable', '8.0.10');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250327021250_CreateProductTable') THEN
    CREATE TABLE "Products" (
        "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
        "Title" character varying(100) NOT NULL,
        "Description" character varying(500) NOT NULL,
        "Category" character varying(50) NOT NULL,
        "Price" numeric(18,2) NOT NULL,
        "Image" character varying(500) NOT NULL,
        "Rate" numeric(3,2) NOT NULL,
        "RatingCount" integer NOT NULL,
        CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250327021250_CreateProductTable') THEN
    CREATE INDEX "IX_Products_Category" ON "Products" ("Category");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250327021250_CreateProductTable') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250327021250_CreateProductTable', '8.0.10');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250330181111_CreateCartsTable') THEN
    CREATE TABLE "Carts" (
        "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
        "UserId" uuid NOT NULL,
        "UserName" character varying(50) NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Carts" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250330181111_CreateCartsTable') THEN
    CREATE TABLE "CartItems" (
        "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
        "ProductId" uuid NOT NULL,
        "ProductTitle" character varying(100) NOT NULL,
        "Quantity" integer NOT NULL,
        "CartId" uuid NOT NULL,
        CONSTRAINT "PK_CartItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_CartItems_Carts_CartId" FOREIGN KEY ("CartId") REFERENCES "Carts" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250330181111_CreateCartsTable') THEN
    CREATE INDEX "IX_CartItems_CartId" ON "CartItems" ("CartId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250330181111_CreateCartsTable') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250330181111_CreateCartsTable', '8.0.10');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250331141324_AlterCartItemsTable') THEN
    ALTER TABLE "CartItems" ADD "UnitPrice" numeric(18,2) NOT NULL DEFAULT 0.0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250331141324_AlterCartItemsTable') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250331141324_AlterCartItemsTable', '8.0.10');
    END IF;
END $EF$;
COMMIT;

DO $EF$
BEGIN
	-- Inserir dados na tabela de Products
	INSERT INTO public."Products"
	("Id", "Title", "Description", "Category", "Price", "Image", "Rate", "RatingCount")
	VALUES
		(gen_random_uuid(), 'Cerveja Lager Premium', 'Uma cerveja lager leve e refrescante.', 'Bebidas', 12.50, 'https://example.com/images/lager.jpg', 4.5, 120),
		(gen_random_uuid(), 'Snacks de Malte', 'Aperitivos feitos com malte de alta qualidade.', 'Alimentos', 8.90, 'https://example.com/images/snack.jpg', 4.2, 85),
		(gen_random_uuid(), 'Copo de Cerveja Artesanal', 'Copo personalizado para degustação de cervejas artesanais.', 'Acessórios', 25.00, 'https://example.com/images/copo.jpg', 4.7, 60),
		(gen_random_uuid(), 'Cerveja IPA Artesanal', 'Uma cerveja artesanal com aroma de lúpulo intenso.', 'Bebidas', 15.90, 'https://example.com/images/ipa.jpg', 4.8, 95),
		(gen_random_uuid(), 'Abridor de Garrafa Premium', 'Abridor de garrafa em aço inoxidável resistente.', 'Acessórios', 19.90, 'https://example.com/images/abridor.jpg', 4.6, 75),
		(gen_random_uuid(), 'Cerveja Stout Escura', 'Uma stout encorpada com notas de café.', 'Bebidas', 18.00, 'https://example.com/images/stout.jpg', 4.7, 85),
		(gen_random_uuid(), 'Kit de Degustação', 'Kit com 4 tipos diferentes de cerveja.', 'Alimentos', 45.00, 'https://example.com/images/kit.jpg', 4.9, 130),
		(gen_random_uuid(), 'Cerveja Pilsen Clássica', 'Uma pilsen tradicional e leve.', 'Bebidas', 10.00, 'https://example.com/images/pilsen.jpg', 4.2, 150),
		(gen_random_uuid(), 'Petiscos para Harmonização', 'Petiscos especiais para acompanhar cervejas.', 'Alimentos', 14.50, 'https://example.com/images/petisco.jpg', 4.4, 90),
		(gen_random_uuid(), 'Growler de Vidro', 'Growler de vidro para transporte de cervejas artesanais.', 'Acessórios', 50.00, 'https://example.com/images/growler.jpg', 4.8, 110),
		(gen_random_uuid(), 'Cerveja Weiss Alemã', 'Uma cerveja de trigo com notas frutadas.', 'Bebidas', 14.50, 'https://example.com/images/weiss.jpg', 4.6, 110),
		(gen_random_uuid(), 'Balde de Gelo Personalizado', 'Balde de gelo personalizado para festas.', 'Acessórios', 39.90, 'https://example.com/images/balde.jpg', 4.5, 80),
		(gen_random_uuid(), 'Snacks Apimentados', 'Aperitivos apimentados para acompanhar cervejas fortes.', 'Alimentos', 9.90, 'https://example.com/images/snacks.jpg', 4.3, 70),
		(gen_random_uuid(), 'Cerveja Red Ale', 'Uma cerveja avermelhada com sabor maltado.', 'Bebidas', 16.00, 'https://example.com/images/redale.jpg', 4.4, 90),
		(gen_random_uuid(), 'Kit de Copos para Cerveja', 'Conjunto de copos para diferentes tipos de cerveja.', 'Acessórios', 60.00, 'https://example.com/images/kitcopos.jpg', 4.9, 100),
		(gen_random_uuid(), 'Cerveja Porter', 'Uma cerveja escura com notas de chocolate.', 'Bebidas', 17.50, 'https://example.com/images/porter.jpg', 4.5, 80),
		(gen_random_uuid(), 'Petiscos Doces', 'Petiscos doces para harmonizar com cervejas escuras.', 'Alimentos', 12.00, 'https://example.com/images/doces.jpg', 4.6, 75),
		(gen_random_uuid(), 'Cerveja Belgian Tripel', 'Uma cerveja belga com alta fermentação.', 'Bebidas', 19.90, 'https://example.com/images/tripel.jpg', 4.8, 70),
		(gen_random_uuid(), 'Copo Térmico', 'Copo térmico para manter sua bebida gelada.', 'Acessórios', 32.00, 'https://example.com/images/termico.jpg', 4.7, 85),
		(gen_random_uuid(), 'Snacks Artesanais', 'Aperitivos artesanais feitos com ingredientes selecionados.', 'Alimentos', 15.00, 'https://example.com/images/artesanal.jpg', 4.5, 95);

END $EF$;
COMMIT;