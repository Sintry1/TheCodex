const axios = require("axios");
import axiosRetry from "axios-retry";

axiosRetry(axios, { retries: 3 });

const create = async ({ name, slot, type, effect }) => {
  try {
    const response = await axios.post(
      "http://localhost:3002/armourDatabase/create",
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
    const response = await axios.get("http://localhost:3002/armourDatabase/");
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const getById = async (id) => {
  try {
    const response = await axios.get(
      `http://localhost:3002/armourDatabase/${id}`
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
      `http://localhost:3002/armourDatabase/${id}`,
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
      `http://localhost:3002/armourDatabase/${id}`
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
