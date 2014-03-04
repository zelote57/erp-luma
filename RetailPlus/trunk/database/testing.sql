SELECT
	DATE_FORMAT(IFNULL(addon.AnniversaryDate,0),'%m')
                ,cntct.ContactID
            ,cntct.ContactCode ,cntct.ContactName ,cntct.BusinessName
            ,grp.ContactGroupID ,grp.ContactGroupName
            ,ModeOfTerms ,cntct.Terms ,cntct.Address
            ,TelephoneNo ,cntct.Remarks ,cntct.Debit ,cntct.Credit ,cntct.CreditLimit ,cntct.IsCreditAllowed
            ,DateCreated ,cntct.Deleted ,dept.DepartmentID ,dept.DepartmentName
            ,pos.PositionID ,pos.PositionName ,cntct.isLock
            ,IFNULL(LastName,'') LastName ,IFNULL(Middlename,'') Middlename ,IFNULL(FirstName,'') FirstName ,IFNULL(Spousename,'') Spousename
            ,SpouseBirthDate ,AnniversaryDate
            ,Address1 ,Address2 ,City ,State ,ZipCode ,IFNULL(cntry.CountryID,0) CountryID ,CountryName
            ,BusinessPhoneNo ,HomePhoneNo ,MobileNo ,FaxNo ,EmailAddress
            ,RewardCardNo ,RewardActive, RewardPoints, RewardAwardDate, TotalPurchases, RedeemedPoints, RewardCardStatus, ExpiryDate
            ,IFNULL(addon.BirthDate,rwrd.BirthDate) BirthDate
    FROM tblContacts cntct
            INNER JOIN tblContactGroup grp ON cntct.ContactGroupID = grp.ContactGroupID
            INNER JOIN tblDepartments dept ON cntct.DepartmentID = dept.DepartmentID
            INNER JOIN tblPositions pos ON cntct.PositionID = pos.PositionID
            LEFT OUTER JOIN tblContactAddOn addon ON addon.ContactID = cntct.ContactID
            LEFT OUTER JOIN tblContactRewards rwrd ON rwrd.CustomerID = cntct.ContactID
            LEFT OUTER JOIN tblCountry cntry ON addon.CountryID = cntry.CountryID
    WHERE 1=1 AND cntct.deleted = 0 AND (ContactGroupCategory = 1 OR ContactGroupCategory = 3) AND DATE_FORMAT(IFNULL(addon.AnniversaryDate,0),'%m') = 1
	
	ORDER BY ContactID  limit 10
	