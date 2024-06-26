const mysql = require("mysql2/promise");
require("dotenv").config();

const pool = mysql.createPool({
  host: process.env.DB_HOST,
  user: process.env.DB_USER,
  password: process.env.DB_PASS,
  database: process.env.DB_NAME,
  port: process.env.DB_PORT,
  waitForConnections: true,
  connectionLimit: 10,
  queueLimit: 0,
});

pool
  .getConnection()
  .then((connection) => {
    console.log("Connected to database");
    connection.release();
  })
  .catch((err) => {
    console.error("Error connecting to database", err);
  });

const query = async (sql, params) => {
    try{
        // Empty variable in the array means we ignore the 2nd parameter returned by the promise, which is "fields" in this case
        const [results, ] = await pool.execute(sql, params);
        return results
    }
    catch(err){
        console.error(err)
        return null
    }  
};

module.exports = {
    query, 
    pool
}
