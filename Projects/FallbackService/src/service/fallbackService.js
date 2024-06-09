const axios = require("axios");

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



module.exports = {
  create
};
