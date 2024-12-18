CREATE DATABASE books_store;
USE books_store;
CREATE TABLE BOOK (
    BOOK_ID INT AUTO_INCREMENT PRIMARY KEY,
    TITLE VARCHAR(200) NOT NULL,
    AUTHORNAME VARCHAR(200) NOT NULL,
    CATEGORY VARCHAR(100) NOT NULL,
    PRICE DECIMAL(10,2) NOT NULL,
    STOCK INT DEFAULT 0,
    DESCRIPTION TEXT,
    IMAGEURL VARCHAR(255),
    RATING DECIMAL(2,1)
);

INSERT INTO BOOK (TITLE, AUTHORNAME, CATEGORY, PRICE, STOCK, DESCRIPTION, IMAGEURL, RATING)
VALUES
('10 Minutes and 38 Seconds in This Strange World', 'Elif Shafak', 'Fiction', 18.99, 25, 'A gripping tale of love, loss, and memory.', 'E:\\forword\\books\\10 min.jfif', 4.7),
('The Alchemist', 'Paulo Coelho', 'Fiction', 16.99, 50, 'A young man’s journey to discover his personal legend.', 'E:\\forword\\books\\alchemist.jfif', 4.9),
('Halim', 'Naguib Mahfouz', 'Fiction', 17.99, 30, 'A story of family and societal change in Cairo.', 'E:\\forword\\books\\halim.jfif', 4.5),
('All the Light We Cannot See', 'Anthony Doerr', 'Fiction', 19.99, 40, 'A WWII story of a blind French girl and a German soldier.', 'E:\\forword\\books\\light.jfif', 4.8),
('And the Mountains Echoed', 'Khaled Hosseini', 'Fiction', 18.99, 35, 'A story about sacrifice, betrayal, and redemption.', 'E:\\forword\\books\\and the mountains echoed.jfif', 4.7),
('Mein Anmol', 'Nemrah Ahmed', 'Fiction', 14.99, 20, 'A love story that crosses cultural boundaries.', 'E:\\forword\\books\\mein anmol.jfif', 4.6),
('As Long As the Lemon Trees Grow', 'Zoulfa Katouh', 'Fiction', 15.99, 28, 'A Syrian girl’s fight for survival in a war-torn world.', 'E:\\forword\\books\\as long as the leon rees grow.jfif', 4.5),
('The Forty Rules of Love', 'Elif Shafak', 'Self-Help', 19.99, 22, 'A spiritual journey inspired by Rumi.', 'E:\\forword\\books\\fourty rules of love.jfif', 4.7),
('Honour', 'Elif Shafak', 'Fiction', 22.99, 10, 'A tale of family, culture, and love.', 'E:\\forword\\books\\honour.jfif', 4.6),
('Harry Potter and the Sorcerer\'s Stone', 'J.K. Rowling', 'Fantasy', 35.99, 50, 'A magical journey into the wizarding world.', 'E:\\forword\\books\\harry potter.jfif', 5.0),
('Healing the Emptiness', 'Yasmin Mogahed', 'Self-Help', 20.99, 15, 'A journey to overcome emotional emptiness.', 'E:\\forword\\books\\healing.jfif', 4.3),
('Iblees', 'Nadia Hashimi', 'Islamic', 18.50, 10, 'A spiritual journey through personal struggles.', 'E:\\forword\\books\\iblees.jfif', 4.4),
('Mein Ne Khwabon Ka Shajr Dekha Hai', 'Nemrah Ahmed', 'Literature', 12.99, 18, 'A collection of emotional and insightful poetry.', 'E:\\forword\\books\\khwabon ka shajr.jfif', 4.6),
('The Kite Runner', 'Khaled Hosseini', 'Fiction', 15.99, 30, 'A gripping tale of friendship and redemption.', 'E:\\forword\\books\\kite runner.jpg', 4.8),
('Love and Happiness', 'Azra Turab', 'Romance', 14.99, 25, 'A love story filled with joy and struggles.', 'E:\\forword\\books\\love and happiness.jfif', 4.5),
('Mala', 'Nemrah Ahmed', 'Fiction', 16.99, 20, 'A tale of love and personal growth.', 'E:\\forword\\books\\mala.jfif', 4.4),
('Manipulation', 'Vera Sten', 'Self-Help', 17.50, 15, 'A guide to understanding and avoiding manipulation.', 'E:\\forword\\books\\manipulation.jfif', 4.2),
('The Manuscript of Accra', 'Paulo Coelho', 'Fiction', 18.99, 40, 'A story about the secrets of life and wisdom.', 'E:\\forword\\books\\manuscript.jfif', 4.6),
('Mere Khwab', 'Nemrah Ahmed', 'Literature', 13.99, 10, 'A collection of poetic dreams and desires.', 'E:\\forword\\books\\mere khwab.jfif', 4.5),
('Mushaf', 'Nemrah Ahmed', 'Islamic', 17.99, 30, 'A woman’s spiritual journey through her faith and family.', 'E:\\forword\\books\\mushaf.jfif', 4.7),
('Namal', 'Nemrah Ahmed', 'Fiction', 19.99, 25, 'A gripping family saga with love, loss, and betrayal.', 'E:\\forword\\books\\namal.jfif', 4.8),
('Paris', 'Nemrah Ahmed', 'Historical Fiction', 22.99, 20, 'The history of Paris from its founding to the present.', 'E:\\forword\\books\\paris.jfif', 4.6),
('Rich Dad Poor Dad', 'Robert Kiyosaki', 'Finance', 25.99, 40, 'A guide to financial independence and wealth building.', 'E:\\forword\\books\\rich dad poor dad.jfif', 4.6),
('Reclaim Your Heart', 'Yasmin Mogahed', 'Self-Help', 18.50, 30, 'A guide to emotional and spiritual healing.', 'E:\\forword\\books\\reclaim ur heart.jfif', 4.7),
('Sacred Path to Islam', 'Sidi Ali', 'Islamic', 14.99, 20, 'A spiritual guide to the Islamic faith and practices.', 'E:\\forword\\books\\sacred path to islam.jfif', 4.5),
('The Secret Sun', 'Saira Shah', 'Fiction', 16.99, 15, 'A historical novel about the power of secrets and truths.', 'E:\\forword\\books\\secret son.jfif', 4.4),
('The Stranger of Olondria', 'Sofia Samatar', 'Fantasy', 18.99, 25, 'A story about adventure, mystery, and magic.', 'E:\\forword\\books\\stranger.jfif', 4.5),
('The Stationery Shop of Tehran', 'Marjan Kamali', 'Fiction', 17.99, 28, 'A love story set against the backdrop of Iranian history.', 'E:\\forword\\books\\the stationary shop.jfif', 4.6),
('A Thousand Splendid Suns', 'Khaled Hosseini', 'Fiction', 18.99, 40, 'A powerful story of resilience, love, and hope.', 'E:\\forword\\books\\thousand splendid suns.jfif', 4.9),
('You Like It Darker', 'Alia M. Dastgir', 'Fiction', 16.50, 18, 'A novel about the complexities of life and choices.', 'E:\\forword\\books\\you like it.jfif', 4.3);

