const axios = require("axios");
const axiosRetry = require("axios-retry").default;


axiosRetry(axios, {
  retries: 3,
  retryDelay: (retryCount) => {
    console.log(`retry attempt: ${retryCount}`);
    return retryCount * 2000;
  },
  retryCondition: (error) => {
    return error.code !== "ECONNABORTED";
  },
});

const create = async ({ name, slot, type, effect }) => {
  try {
    const response = await axios.post(
      "http://localhost:3002/armourDatabase/create",
      { name, slot, type, effect }
    );
    console.log(response.data);
    return response.data;
  } catch (e) {
    if (e.code === "ECONNABORTED") {
      console.error("Request failed after 3 retries");
    } else {
      console.error(`Error in create: ${e.code} - ${e.message}`);
    }
  }
};

const getAll = async () => {
  try {
    const response = await axios.get("http://localhost:3002/armourDatabase/");
    console.log(response.data);
    return response.data;
  } catch (e) {
    if (e.code === "ECONNABORTED") {
      console.error("Request failed after 3 retries");
    } else {
      console.error(`Error in getAll: ${e.code} - ${e.message}`);
    }
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
    if (e.code === "ECONNABORTED") {
      console.error("Request failed after 3 retries");
    } else {
      console.error(`Error in getById: ${e.code} - ${e.message}`);
    }
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
    if (e.code === "ECONNABORTED") {
      console.error("Request failed after 3 retries");
    } else {
      console.error(`Error in update: ${e.code} - ${e.message}`);
    }
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
    if (e.code === "ECONNABORTED") {
      console.error("Request failed after 3 retries");
    } else {
      console.error(`Error in deleteArmour: ${e.code} - ${e.message}`);
    }
  }
};

module.exports = {
  create,
  getAll,
  getById,
  update,
  deleteArmour,
};
