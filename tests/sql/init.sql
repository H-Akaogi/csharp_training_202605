TRUNCATE TABLE
    item_stock,
    item,
    item_category
RESTART IDENTITY CASCADE;

INSERT INTO item_category (name) VALUES ('文房具');
INSERT INTO item_category (name) VALUES ('雑貨');
INSERT INTO item_category (name) VALUES ('パソコン周辺機器');

INSERT INTO item (name, price, category_id) VALUES ('水性ボールペン(黒)', 120, 1);
INSERT INTO item (name, price, category_id) VALUES ('ワンタッチ開閉傘', 3000, 2);
INSERT INTO item (name, price, category_id) VALUES ('ワイヤレスマウス', 900, 3);

INSERT INTO item_stock (stock, item_id) VALUES (20, 1);
INSERT INTO item_stock (stock, item_id) VALUES (10, 2);
INSERT INTO item_stock (stock, item_id) VALUES (12, 3);
