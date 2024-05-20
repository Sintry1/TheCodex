const db = require("../infra/connection");


const create = async ({name, slot, type, effect}) => {
  try {
    const result = await db.query(
      `INSERT INTO armour (name, slot, type, effect) VALUES ('${name}', '${slot}', '${type}', '${effect}')`
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

const update = async (id, name, slot, type, effect) => {
    try {
        const result = await db.query(
            `UPDATE armour SET name = '${name}', slot = '${slot}', type = '${type}', effect = '${effect}' WHERE id = ${id}`
        );
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
    deleteArmour
}