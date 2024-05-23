const db = require("../infra/connection");


const create = async ({ name, type, effect }) => {
    try {
      const effectValue = effect == null ? "NULL" : `'${effect}'`;
      const result = await db.query(
        `INSERT INTO jewellery (name, type, effect) VALUES ('${name}', '${type}', ${effectValue})`
      );
      return result;
    } catch (e) {
      console.log(e);
    }
  };

const getAll = async () => {
    try {
        const result = await db.query(`SELECT * FROM jewellery`);
        return result;
    } catch (e) {
        console.log(e);
    }
};

const getById = async (id) => {
    try {
        const result = await db.query(`SELECT * FROM jewellery WHERE id = ${id}`);
        return result;
    } catch (e) {
        console.log(e);
    }
};

const update = async (id, { name, type, effect }) => {
    try {
      let query = 'UPDATE jewellery SET ';
      if (name !== undefined) query += `name = '${name}', `;
      if (type !== undefined) query += `type = '${type}', `;
      if (effect !== undefined) {
        const effectValue = effect == null ? "NULL" : `'${effect}'`;
        query += `effect = ${effectValue}, `;
      }
      query = query.slice(0, -2) + ` WHERE id = ${id}`;
      const result = await db.query(query);
      return result;
    } catch (e) {
      console.log(e);
    }
  };

  const deleteJewellery = async (id) => {
    try {
      const result = await db.query(`DELETE FROM jewellery WHERE id = ${id}`);
      return result;
    } catch (e) {
      console.log(e);
    }
  }

module.exports = {
    create,
    getAll,
    getById,
    update,
    deleteJewellery
}