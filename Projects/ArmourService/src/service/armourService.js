const axios = require("axios");

const create = async ({ name, slot, type, effect }) => {
  try {
    const response = await axios.post(
      "http://localhost:3002/armour/create",
      { name, slot, type, effect }
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const getAll = async () => {
  try {
    const response = await axios.get("http://localhost:3002/armour/");
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const getById = async (id) => {
  try {
    const response = await axios.get(
      `http://localhost:3002/armour/${id}`
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const update = async (id, { name, slot, type, effect }) => {
  try {
    const response = await axios.put(
      `http://localhost:3002/armour/${id}`,
      { name, slot, type, effect }
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const deleteArmour = async (id) => {
  try {
    const response = await axios.delete(
      `http://localhost:3002/armour/${id}`
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

module.exports = {
  create,
  getAll,
  getById,
  update,
  deleteArmour,
};
