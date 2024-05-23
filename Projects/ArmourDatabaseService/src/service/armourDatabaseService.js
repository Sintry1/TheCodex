const db = require("../infra/connection");

const create = async ({ name, slot, type, effect }) => {
  try {
    // If effect is undefined or null, convert it to NULL (for SQL)
    const effectValue = effect == null ? "NULL" : `'${effect}'`;

    const result = await db.query(
      `INSERT INTO armour (name, slot, type, effect) VALUES ('${name}', '${slot}', '${type}', ${effectValue})`
    );
    return result;
  } catch (e) {
    console.log(e);
  }
};

const getAll = async () => {
  try {
    const result = await db.query(`SELECT * FROM armour`);
    return result;
  } catch (e) {
    console.log(e);
  }
};

const getById = async (id) => {
  try {
    const result = await db.query(`SELECT * FROM armour WHERE id = ${id}`);
    return result;
  } catch (e) {
    console.log(e);
  }
};

const update = async (id, { name, slot, type, effect }) => {
    try {
      // Start building the SQL query
      let query = 'UPDATE armour SET ';
  
      // Add each property to the query if it's not undefined
      if (name !== undefined) query += `name = '${name}', `;
      if (slot !== undefined) query += `slot = '${slot}', `;
      if (type !== undefined) query += `type = '${type}', `;
      if (effect !== undefined) {
        const effectValue = effect == null ? "NULL" : `'${effect}'`;
        query += `effect = ${effectValue}, `;
      }
  
      // Remove the trailing comma and space, add the WHERE clause
      query = query.slice(0, -2) + ` WHERE id = ${id}`;
  
      const result = await db.query(query);
      return result;
    } catch (e) {
      console.log(e);
    }
  };

const deleteArmour = async (id) => {
  try {
    const result = await db.query(`DELETE FROM armour WHERE id = ${id}`);
    return result;
  } catch (e) {
    console.log(e);
  }
};

module.exports = {
  create,
  getAll,
  getById,
  update,
  deleteArmour,
};
