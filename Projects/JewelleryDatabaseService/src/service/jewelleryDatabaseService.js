const db = require("../infra/connection");


const create = async ({name, type, effect}) => {
  try {
    const result = await db.query(
      `INSERT INTO jewellery (name, type, effect) VALUES ('${name}', '${type}', '${effect}')`
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

const update = async (id, name, type, effect) => {
    try {
        const result = await db.query(
            `UPDATE jewellery SET name = '${name}', type = '${type}', effect = '${effect}' WHERE id = ${id}`
        );
        return result;
    } catch (e) {
        console.log(e);
    }
};

const deleteArmour = async (id) => {
    try {
        const result = await db.query(`DELETE FROM jewellery WHERE id = ${id}`);
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
    deleteArmour
}