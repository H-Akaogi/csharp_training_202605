TRUNCATE TABLE
    department,
    employee
RESTART IDENTITY CASCADE;

INSERT INTO department (name) VALUES ('総務部');
INSERT INTO department (name) VALUES ('経理部');
INSERT INTO department (name) VALUES ('人事部');
INSERT INTO department (name) VALUES ('開発部');
INSERT INTO department (name) VALUES ('営業部');

INSERT INTO employee (name, dept_id,email,phone) VALUES ('田中太郎',2,'tanakatarou@example.com','090-0000-0001');
INSERT INTO employee (name, dept_id,email,phone) VALUES ('鈴木三郎',1,'suzukisaburou@example.com','090-0000-0002');
INSERT INTO employee (name, dept_id,email,phone) VALUES ('佐藤花子',4,'sastouhanako@example.com','090-0000-0003');
INSERT INTO employee (name, dept_id,email,phone) VALUES ('中田彩子',5,'nakataayako@example.com','090-0000-0004');
INSERT INTO employee (name, dept_id,email,phone) VALUES ('加藤圭太',3,'katoukeita@example.com','090-0000-0005');
INSERT INTO employee (name, dept_id,email,phone) VALUES ('松本良太',4,'matumotoryouta@example.com','090-0000-0006');
INSERT INTO employee (name, dept_id,email,phone) VALUES ('山下孝輔',5,'yamasitakousuke@example.com','090-0000-0007');
INSERT INTO employee (name, dept_id,email,phone) VALUES ('渡辺大輔',4,'watanabedaisuke@example.com','090-0000-0008');