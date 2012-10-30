/*****************************
**	sysConfig
*****************************/
DROP TABLE IF EXISTS sysConfig;
CREATE TABLE sysConfig (
	`ConfigName` VARCHAR(30) NOT NULL,
	`ConfigValue` VARCHAR(150) NOT NULL,
	PRIMARY KEY (ConfigName),
	INDEX `IX_sysConfig`(`ConfigName`),
	UNIQUE `PK_sysConfig`(`ConfigName`)
);

INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('CompanyCode',						'RBS');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('CompanyName',						'RETAILPLUS BUSINESS SOLUTIONS');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('TIN',								'104-384-077-000');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('Currency',							'PHP');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('VersionFTPIPAddress',			    'Localhost');

