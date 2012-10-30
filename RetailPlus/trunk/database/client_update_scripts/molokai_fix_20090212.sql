UPDATE tblTerminal SET MachineSerialNo = '000000';
UPDATE tblTerminal SET AccreditationNo = '0000-000-00000-000';
 
 -- February 12, 2009
 -- Fix the transactions for Feb 11 and 12 coz the date was set to FEB 17 and 18.

-- GET TRANSACTIONDATE THAT WILL BE AFFECTED
SELECT TransactionNo, TransactionDate FROM tblTransactions02 WHERE TransactionNo >= 00000000036767 AND TransactionNo <= 00000000036812;

-- UPDATE tblTransactions02
UPDATE tblTransactions02 SET TransactionDate = DATE_ADD(TransactionDate, INTERVAL -6 DAY) WHERE TransactionNo >= 00000000036767 AND TransactionNo <= 00000000036812; 

-- GET TRANSACTIONDATE THAT WILL BE AFFECTED
SELECT TransactionNo, TransactionDate FROM tblTransactions02 WHERE TransactionNo >= 00000000036767 AND TransactionNo <= 00000000036812;

-- UPDATE tblTerminalReportHistory
UPDATE tblTerminalReportHistory SET DateLastInitialized = '2009-02-11 00:06:36' WHERE DateLastInitialized = '2009-02-17 00:06:36';

-- UPDATE tblTerminalReport 
UPDATE tblTerminalReport SET DateLastInitialized = '2009-02-12 00:06:36' WHERE DateLastInitialized = '2009-02-18 00:46:21';

-- UPDATE tblcashierreporthistory 
UPDATE tblcashierreporthistory SET lastlogindate = '2009-02-10 16:14:52' where lastlogindate = '2010-02-10 16:14:52';
UPDATE tblcashierreporthistory SET lastlogindate = '2009-02-11 01:14:36' where lastlogindate = '2009-02-17 01:14:36';
UPDATE tblcashierreporthistory SET lastlogindate = '2009-02-11 00:07:45' where lastlogindate = '2009-02-17 00:07:45';

-- UPDATE tblcashierreport
UPDATE tblcashierreport SET lastlogindate = '2009-02-10 16:14:52' where lastlogindate = '2010-02-10 16:14:52';
UPDATE tblcashierreport SET lastlogindate = '2009-02-12 00:04:54' where lastlogindate = '2009-02-19 00:04:54';
UPDATE tblcashierreport SET lastlogindate = '2009-02-11 01:07:05' where lastlogindate = '2009-02-18 01:07:05';
UPDATE tblcashierreport SET lastlogindate = '2009-02-11 00:47:20' where lastlogindate = '2009-02-18 00:47:20';
 
UPDATE tblcashierreport SET TerminalID = 1;
UPDATE tblcashierreporthistory SET TerminalID = 1;



/*

select lastlogindate, tblcashierreporthistory.* from tblcashierreporthistory order by lastlogindate desc limit 5;
select lastlogindate, tblcashierreport.* from tblcashierreport order by lastlogindate desc;
SELECT DateLastInitialized FROM tblTerminalReportHistory LIMIT 5;
SELECT DateLastInitialized FROM tblTerminalReport;

SELECT TransactionID, TransactionNo, TransactionDate FROM tblTransactions02 
WHERE TransactionNo >= (SELECT BeginningTransactionNo FROM tblTerminalReportHistory WHERE DateLastInitialized = '2009-02-17 00:06:36')
AND TransactionNo < (SELECT EndingTransactionNo FROM tblTerminalReportHistory WHERE DateLastInitialized = '2009-02-17 00:06:36');

+---------------+----------------+---------------------+---------------------+
| TransactionID | TransactionNo  | TransactionDate     | NewTransactionDate  |
+---------------+----------------+---------------------+---------------------+
|          2900 | 00000000036767 | 2009-02-17 00:16:59 | 2009-02-11 00:16:59 |
|          2901 | 00000000036768 | 2009-02-17 00:17:59 | 2009-02-11 00:17:59 |
|          2902 | 00000000036769 | 2009-02-17 00:18:28 | 2009-02-11 00:18:28 |
|          2903 | 00000000036770 | 2009-02-17 00:19:28 | 2009-02-11 00:19:28 |
|          2904 | 00000000036771 | 2009-02-17 00:22:54 | 2009-02-11 00:22:54 |
|          2905 | 00000000036772 | 2009-02-17 00:26:53 | 2009-02-11 00:26:53 |
|          2906 | 00000000036773 | 2009-02-17 00:32:01 | 2009-02-11 00:32:01 |
|          2907 | 00000000036774 | 2009-02-17 00:33:53 | 2009-02-11 00:33:53 |
|          2908 | 00000000036775 | 2009-02-17 00:36:58 | 2009-02-11 00:36:58 |
|          2909 | 00000000036776 | 2009-02-17 01:21:56 | 2009-02-11 01:21:56 |
|          2910 | 00000000036777 | 2009-02-17 01:59:50 | 2009-02-11 01:59:50 |
|          2911 | 00000000036778 | 2009-02-17 08:46:02 | 2009-02-11 08:46:02 |
|          2912 | 00000000036779 | 2009-02-17 08:48:19 | 2009-02-11 08:48:19 |
|          2913 | 00000000036780 | 2009-02-17 16:43:22 | 2009-02-11 16:43:22 |
|          2914 | 00000000036781 | 2009-02-17 16:44:52 | 2009-02-11 16:44:52 |
|          2915 | 00000000036782 | 2009-02-17 17:09:59 | 2009-02-11 17:09:59 |
|          2916 | 00000000036783 | 2009-02-17 18:14:53 | 2009-02-11 18:14:53 |
|          2917 | 00000000036784 | 2009-02-17 18:23:40 | 2009-02-11 18:23:40 |
|          2918 | 00000000036785 | 2009-02-17 18:34:21 | 2009-02-11 18:34:21 |
|          2919 | 00000000036786 | 2009-02-17 18:38:45 | 2009-02-11 18:38:45 |
|          2920 | 00000000036787 | 2009-02-17 18:43:08 | 2009-02-11 18:43:08 |
|          2921 | 00000000036788 | 2009-02-17 18:55:44 | 2009-02-11 18:55:44 |
|          2922 | 00000000036789 | 2009-02-17 19:08:30 | 2009-02-11 19:08:30 |
|          2923 | 00000000036790 | 2009-02-17 19:09:40 | 2009-02-11 19:09:40 |
|          2924 | 00000000036791 | 2009-02-17 19:28:25 | 2009-02-11 19:28:25 |
|          2925 | 00000000036792 | 2009-02-17 19:31:00 | 2009-02-11 19:31:00 |
|          2926 | 00000000036793 | 2009-02-17 19:39:17 | 2009-02-11 19:39:17 |
|          2927 | 00000000036794 | 2009-02-17 19:57:02 | 2009-02-11 19:57:02 |
|          2928 | 00000000036795 | 2009-02-17 20:03:18 | 2009-02-11 20:03:18 |
|          2929 | 00000000036796 | 2009-02-17 20:08:13 | 2009-02-11 20:08:13 |
|          2930 | 00000000036797 | 2009-02-17 20:37:01 | 2009-02-11 20:37:01 |
|          2931 | 00000000036798 | 2009-02-17 20:57:49 | 2009-02-11 20:57:49 |
|          2932 | 00000000036799 | 2009-02-17 20:58:51 | 2009-02-11 20:58:51 |
|          2933 | 00000000036800 | 2009-02-17 21:30:54 | 2009-02-11 21:30:54 |
|          2934 | 00000000036801 | 2009-02-17 22:04:36 | 2009-02-11 22:04:36 |
|          2935 | 00000000036802 | 2009-02-17 22:23:36 | 2009-02-11 22:23:36 |
|          2936 | 00000000036803 | 2009-02-17 22:40:07 | 2009-02-11 22:40:07 |
|          2937 | 00000000036804 | 2009-02-17 22:48:29 | 2009-02-11 22:48:29 |
|          2938 | 00000000036805 | 2009-02-17 22:53:54 | 2009-02-11 22:53:54 |
|          2939 | 00000000036806 | 2009-02-17 23:01:20 | 2009-02-11 23:01:20 |
|          2940 | 00000000036807 | 2009-02-17 23:04:31 | 2009-02-11 23:04:31 |
|          2941 | 00000000036808 | 2009-02-17 23:18:37 | 2009-02-11 23:18:37 |
|          2942 | 00000000036809 | 2009-02-17 23:26:22 | 2009-02-11 23:26:22 |
|          2943 | 00000000036810 | 2009-02-17 23:38:04 | 2009-02-11 23:38:04 |
|          2944 | 00000000036811 | 2009-02-17 23:48:53 | 2009-02-11 23:48:53 |
|          2945 | 00000000036812 | 2009-02-18 00:23:54 | 2009-02-12 00:23:54 |
+---------------+----------------+---------------------+---------------------+
*/