const axios = require("axios");
import axiosRetry from "axios-retry";

axiosRetry(axios, { retries: 3 });

const create = async ({ name, type, effect }) => {
  try {
    const response = await axios.post(
      "http://localhost:3004/jewelleryDatabase/create",
      { name, type, effect }
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const getAll = async () => {
  try {
    const response = await axios.get(
      "http://localhost:3004/jewelleryDatabase/"
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const getById = async (id) => {
  try {
    const response = await axios.get(
      `http://localhost:3004/jewelleryDatabase/${id}`
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    console.error(e);
  }
};

const update = async (id, { name, type, effect }) => {
  try {
    const response = await axios.put(
      `http://localhost:3004/jewelleryDatabase/${id}`,
      { name, type, effect }
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
      `http://localhost:3004/jewelleryDatabase/${id}`
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
