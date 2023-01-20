CREATE  TABLE "audit" ( 
	id                   uuid  NOT NULL  ,
	created              date  NOT NULL  ,
	detail               varchar  NOT NULL  ,
	userid               varchar    ,
	CONSTRAINT audit_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE cients ( 
	id                   uuid  NOT NULL  ,
	contactname          varchar  NOT NULL  ,
	name                 varchar  NOT NULL  ,
	contactemail         varchar  NOT NULL  ,
	CONSTRAINT client_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE notifications ( 
	id                   uuid  NOT NULL  ,
	created              date  NOT NULL  ,
	status               char(1)  NOT NULL  ,
	nextretrydate        date    ,
	content              varchar  NOT NULL  ,
	title                varchar  NOT NULL  ,
	CONSTRAINT notifications_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE projectroles ( 
	id                   uuid  NOT NULL  ,
	name                 varchar  NOT NULL  ,
	technical            boolean  NOT NULL  ,
	description          varchar  NOT NULL  ,
	CONSTRAINT projectroles_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE projects ( 
	id                   uuid  NOT NULL  ,
	created              date  NOT NULL  ,
	name                 varchar  NOT NULL  ,
	reference            varchar  NOT NULL  ,
	startdate            date  NOT NULL  ,
	enddate              date  NOT NULL  ,
	contractnumber       varchar    ,
	CONSTRAINT projects_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE recipients ( 
	id                   uuid  NOT NULL  ,
	name                 varchar  NOT NULL  ,
	email                varchar  NOT NULL  ,
	notificationid       uuid  NOT NULL  ,
	CONSTRAINT recipients_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE sows ( 
	id                   uuid  NOT NULL  ,
	created              date    ,
	"file"               bytea  NOT NULL  ,
	ischangerequest      boolean  NOT NULL  ,
	sow_startdate        date  NOT NULL  ,
	sow_enddate          date  NOT NULL  ,
	projectid            uuid  NOT NULL  ,
	CONSTRAINT sows_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE usergroups ( 
	id                   uuid  NOT NULL  ,
	name                 varchar  NOT NULL  ,
	receivesreports      boolean    ,
	CONSTRAINT usergroup_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE users ( 
	id                   uuid  NOT NULL  ,
	firstname            varchar  NOT NULL  ,
	lastname             varchar  NOT NULL  ,
	email                varchar  NOT NULL  ,
	usergroupid          uuid  NOT NULL  ,
	CONSTRAINT users_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE clientprojects ( 
	id                   uuid  NOT NULL  ,
	clientid             uuid  NOT NULL  ,
	projectid            uuid  NOT NULL  ,
	CONSTRAINT clientprojects_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE contacts ( 
	id                   uuid  NOT NULL  ,
	firstname            varchar  NOT NULL  ,
	email                varchar  NOT NULL  ,
	receivesreport       boolean  NOT NULL  ,
	projectid            uuid  NOT NULL  ,
	CONSTRAINT contacts_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE reports ( 
	id                   uuid  NOT NULL  ,
	created              date  NOT NULL  ,
	plannedtasks         varchar  NOT NULL  ,
	completedtasks       varchar  NOT NULL  ,
	weeknumber           integer  NOT NULL  ,
	submissiondate       date    ,
	projectid            uuid  NOT NULL  ,
	userid               uuid  NOT NULL  ,
	CONSTRAINT reports_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE risks ( 
	id                   uuid  NOT NULL  ,
	reportid             uuid  NOT NULL  ,
	riskdetails          varchar  NOT NULL  ,
	riskmitigation       varchar  NOT NULL  ,
	ragstatus            char(1)    ,
	CONSTRAINT risks_pkey PRIMARY KEY ( id )
 );

CREATE  TABLE userprojectroles ( 
	userid               uuid  NOT NULL  ,
	projectid            uuid  NOT NULL  ,
	projectroleid        uuid  NOT NULL  ,
	recievesreports      boolean  NOT NULL  
 );

ALTER TABLE clientprojects ADD CONSTRAINT fk_client_to_clientprojects FOREIGN KEY ( clientid ) REFERENCES cients( id );

ALTER TABLE clientprojects ADD CONSTRAINT fk_projects_to_clientprojects FOREIGN KEY ( projectid ) REFERENCES projects( id );

ALTER TABLE contacts ADD CONSTRAINT fk_projects_to_contacts FOREIGN KEY ( projectid ) REFERENCES projects( id );

ALTER TABLE recipients ADD CONSTRAINT fk_notifications_to_recipients FOREIGN KEY ( notificationid ) REFERENCES notifications( id );

ALTER TABLE reports ADD CONSTRAINT fk_projects_to_reports FOREIGN KEY ( projectid ) REFERENCES projects( id );

ALTER TABLE reports ADD CONSTRAINT fk_users_to_reports FOREIGN KEY ( userid ) REFERENCES users( id );

ALTER TABLE risks ADD CONSTRAINT fk_reports_to_risks FOREIGN KEY ( reportid ) REFERENCES reports( id );

ALTER TABLE sows ADD CONSTRAINT fk_projects_to_sows FOREIGN KEY ( projectid ) REFERENCES projects( id );

ALTER TABLE userprojectroles ADD CONSTRAINT fk_projectroles_to_userprojectroles FOREIGN KEY ( projectroleid ) REFERENCES projectroles( id );

ALTER TABLE userprojectroles ADD CONSTRAINT fk_projects_to_userprojectroles FOREIGN KEY ( projectid ) REFERENCES projects( id );

ALTER TABLE userprojectroles ADD CONSTRAINT fk_users_to_userprojectroles FOREIGN KEY ( userid ) REFERENCES users( id );

ALTER TABLE users ADD CONSTRAINT fk_usergroup_to_users FOREIGN KEY ( usergroupid ) REFERENCES usergroups( id );

COMMENT ON TABLE cients IS 'For example DfE, ESFA';

COMMENT ON COLUMN notifications.status IS 'Pending,Sent,Failed';

COMMENT ON TABLE projectroles IS 'A reference table detailing the project roles that Talent offer to the client. All users will have at least one role per project. A user can be across many projects';

COMMENT ON COLUMN risks.ragstatus IS 'Red/Amber/Green';

INSERT INTO cients( id, contactname, name, contactemail ) VALUES ( '83c756a8-ff87-48be-a862-096678b41817', 'Harry Potter', 'DfE', 'harry@potter.com');
INSERT INTO cients( id, contactname, name, contactemail ) VALUES ( 'e24a5543-6368-490a-a1d0-a18f0c69848a', 'Hermione Granger', 'ESFA', 'hermione@granger.com');
INSERT INTO projectroles( id, name, technical, description ) VALUES ( '626bff24-61c7-49d3-81bd-4aa12311e103', 'Developer', 't', 'Developer on the project writing the code');
INSERT INTO projectroles( id, name, technical, description ) VALUES ( 'fe32f237-ce7e-48f2-add7-fa5dc725396c', 'Architect', 't', 'Over seeing the architecture of the project');
INSERT INTO projectroles( id, name, technical, description ) VALUES ( '8ca4c6cf-6d3a-43cd-85c1-866557c7384e', 'Business Analyst', 'f', 'Analysing business needs');
INSERT INTO projectroles( id, name, technical, description ) VALUES ( 'f703d11b-6183-4c67-81c5-adbf2070a016', 'Delivery Manager', 'f', 'Overseeing the delivery of the project');
INSERT INTO projects( id, created, name, reference, startdate, enddate, contractnumber ) VALUES ( '86b610ee-e866-4749-9f10-4a5c59e96f2f', '2023-01-18', 'Social work CPD', 'con_23sds', '2023-10-01', '2023-03-31', 'cona_sdsafds');
INSERT INTO usergroups( id, name, receivesreports ) VALUES ( '2a91939a-57fd-4049-afa9-88e547c5bd92', 'Global Administrator', 't');
INSERT INTO usergroups( id, name, receivesreports ) VALUES ( '3a38a77c-3bda-4950-8802-e1b636c4c29f', 'Project Admin', 't');
INSERT INTO usergroups( id, name, receivesreports ) VALUES ( '768aa546-ec03-4663-b7f4-26569932b2af', 'User', 'f');
INSERT INTO contacts( id, firstname, email, receivesreport, projectid ) VALUES ( '03a33a03-a98d-4946-8e8f-05cbc7a949b6', 'Ron Weasley\n', 'ron@weasley.com', 't', '86b610ee-e866-4749-9f10-4a5c59e96f2f');