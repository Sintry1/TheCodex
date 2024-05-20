const express = require('express');
const cors = require('cors');
const armourDatabaseController = require('./src/controller/armourDatabaseController');  // replace with the actual path to your controller file

const PORT = 3002;
const app = express();

app.use(cors());
app.use(express.json());

// Use your router for requests to a certain path
app.use('/armourDatabase', armourDatabaseController);  // replace '/armour' with the path you want

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});