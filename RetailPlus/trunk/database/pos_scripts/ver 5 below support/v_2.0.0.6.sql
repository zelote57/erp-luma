/*********************************  v_2.0.0.6.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.6';


ALTER TABLE sysAccessTypes DROP SequenceNo; 
ALTER TABLE sysAccessTypes DROP Category;

ALTER TABLE sysAccessTypes ADD SequenceNo INT NOT NULL DEFAULT 0; 
ALTER TABLE sysAccessTypes ADD Category VARCHAR(35) NOT NULL DEFAULT 'System Configurations'; 

UPDATE sysAccessTypes SET SequenceNo = 1, Category = '01: System Configurations' WHERE TypeID = 1;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '01: System Configurations' WHERE TypeID = 43;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '01: System Configurations' WHERE TypeID = 48;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '01: System Configurations' WHERE TypeID = 123;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '01: System Configurations' WHERE TypeID = 47;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '01: System Configurations' WHERE TypeID = 82;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '01: System Configurations' WHERE TypeID = 49;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '01: System Configurations' WHERE TypeID = 77;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '02: Backend - Administration' WHERE TypeID = 46;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '02: Backend - Administration' WHERE TypeID = 44;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '02: Backend - Administration' WHERE TypeID = 45;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '03: Backend - Menu' WHERE TypeID = 2;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '03: Backend - Menu' WHERE TypeID = 3;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '03: Backend - Menu' WHERE TypeID = 93;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '03: Backend - Menu' WHERE TypeID = 104;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '03: Backend - Menu' WHERE TypeID = 111;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '03: Backend - Menu' WHERE TypeID = 103;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '03: Backend - Menu' WHERE TypeID = 20;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '03: Backend - Menu' WHERE TypeID = 24;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '03: Backend - Menu' WHERE TypeID = 42;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '04: Backend - MasterFiles' WHERE TypeID = 4;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '04: Backend - MasterFiles' WHERE TypeID = 5;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '04: Backend - MasterFiles' WHERE TypeID = 16;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '04: Backend - MasterFiles' WHERE TypeID = 17;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '04: Backend - MasterFiles' WHERE TypeID = 18;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '04: Backend - MasterFiles' WHERE TypeID = 19;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '04: Backend - MasterFiles' WHERE TypeID = 88;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '04: Backend - MasterFiles' WHERE TypeID = 119;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 12;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 13;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 14;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 15;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 10;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 11;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 8;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 9;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 7;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 6;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 92;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 122;
UPDATE sysAccessTypes SET SequenceNo = 13, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 127;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 94;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 105;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 106;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 107;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 109;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 108;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 110;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '08: Backend - Inventory' WHERE TypeID = 21;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '08: Backend - Inventory' WHERE TypeID = 22;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '08: Backend - Inventory' WHERE TypeID = 23;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '08: Backend - Inventory' WHERE TypeID = 89;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '08: Backend - Inventory' WHERE TypeID = 90;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '08: Backend - Inventory' WHERE TypeID = 112;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '08: Backend - Inventory' WHERE TypeID = 113;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '08: Backend - Inventory' WHERE TypeID = 114;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '08: Backend - Inventory' WHERE TypeID = 115;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '08: Backend - Inventory' WHERE TypeID = 116;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '08: Backend - Inventory' WHERE TypeID = 117;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '08: Backend - Inventory' WHERE TypeID = 118;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '09: Backend - General Ledger' WHERE TypeID = 99;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '09: Backend - General Ledger' WHERE TypeID = 100;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '09: Backend - General Ledger' WHERE TypeID = 101;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '09: Backend - General Ledger' WHERE TypeID = 97;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '09: Backend - General Ledger' WHERE TypeID = 102;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '09: Backend - General Ledger' WHERE TypeID = 98;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '09: Backend - General Ledger' WHERE TypeID = 96;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '09: Backend - General Ledger' WHERE TypeID = 95;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '10: Backend - Inventory Reports' WHERE TypeID = 25;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '10: Backend - Inventory Reports' WHERE TypeID = 26;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '10: Backend - Inventory Reports' WHERE TypeID = 27;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '10: Backend - Inventory Reports' WHERE TypeID = 28;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '10: Backend - Inventory Reports' WHERE TypeID = 29;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '10: Backend - Inventory Reports' WHERE TypeID = 34;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '10: Backend - Inventory Reports' WHERE TypeID = 35;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '10: Backend - Inventory Reports' WHERE TypeID = 121;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '10: Backend - Inventory Reports' WHERE TypeID = 38;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '10: Backend - Inventory Reports' WHERE TypeID = 39;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '10: Backend - Inventory Reports' WHERE TypeID = 40;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '11: Backend - Sales Reports' WHERE TypeID = 41;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '11: Backend - Sales Reports' WHERE TypeID = 30;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '11: Backend - Sales Reports' WHERE TypeID = 36;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '12: Backend - Admin Reports' WHERE TypeID = 31;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '12: Backend - Admin Reports' WHERE TypeID = 32;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '12: Backend - Admin Reports' WHERE TypeID = 33;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '12: Backend - Admin Reports' WHERE TypeID = 86;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '13: Frontend - Administration' WHERE TypeID = 50;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '13: Frontend - Administration' WHERE TypeID = 51;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '13: Frontend - Administration' WHERE TypeID = 78;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '13: Frontend - Administration' WHERE TypeID = 79;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '13: Frontend - Administration' WHERE TypeID = 67;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '13: Frontend - Administration' WHERE TypeID = 126;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '13: Frontend - Administration' WHERE TypeID = 81;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '14: Frontend - Cashiering' WHERE TypeID = 52;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '14: Frontend - Cashiering' WHERE TypeID = 53;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '14: Frontend - Cashiering' WHERE TypeID = 54;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '14: Frontend - Cashiering' WHERE TypeID = 55;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '14: Frontend - Cashiering' WHERE TypeID = 56;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '14: Frontend - Cashiering' WHERE TypeID = 57;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '14: Frontend - Cashiering' WHERE TypeID = 58;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '14: Frontend - Cashiering' WHERE TypeID = 59;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '14: Frontend - Cashiering' WHERE TypeID = 60;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '14: Frontend - Cashiering' WHERE TypeID = 61;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '14: Frontend - Cashiering' WHERE TypeID = 62;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '14: Frontend - Cashiering' WHERE TypeID = 63;
UPDATE sysAccessTypes SET SequenceNo = 13, Category = '14: Frontend - Cashiering' WHERE TypeID = 64;
UPDATE sysAccessTypes SET SequenceNo = 14, Category = '14: Frontend - Cashiering' WHERE TypeID = 65;
UPDATE sysAccessTypes SET SequenceNo = 15, Category = '14: Frontend - Cashiering' WHERE TypeID = 66;
UPDATE sysAccessTypes SET SequenceNo = 16, Category = '14: Frontend - Cashiering' WHERE TypeID = 68;
UPDATE sysAccessTypes SET SequenceNo = 17, Category = '14: Frontend - Cashiering' WHERE TypeID = 80;
UPDATE sysAccessTypes SET SequenceNo = 18, Category = '14: Frontend - Cashiering' WHERE TypeID = 83;
UPDATE sysAccessTypes SET SequenceNo = 19, Category = '14: Frontend - Cashiering' WHERE TypeID = 84;
UPDATE sysAccessTypes SET SequenceNo = 20, Category = '14: Frontend - Cashiering' WHERE TypeID = 85;
UPDATE sysAccessTypes SET SequenceNo = 21, Category = '14: Frontend - Cashiering' WHERE TypeID = 87;
UPDATE sysAccessTypes SET SequenceNo = 22, Category = '14: Frontend - Cashiering' WHERE TypeID = 91;
UPDATE sysAccessTypes SET SequenceNo = 23, Category = '14: Frontend - Cashiering' WHERE TypeID = 120;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '15: Frontend - Reports' WHERE TypeID = 69;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '15: Frontend - Reports' WHERE TypeID = 70;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '15: Frontend - Reports' WHERE TypeID = 71;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '15: Frontend - Reports' WHERE TypeID = 72;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '15: Frontend - Reports' WHERE TypeID = 73;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '15: Frontend - Reports' WHERE TypeID = 74;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '15: Frontend - Reports' WHERE TypeID = 75;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '15: Frontend - Reports' WHERE TypeID = 76;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '15: Frontend - Reports' WHERE TypeID = 124;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '15: Frontend - Reports' WHERE TypeID = 125;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '15: Frontend - Reports' WHERE TypeID = 37;

        
/*********************************  v_2.0.0.6.sql END  *******************************************************/ 