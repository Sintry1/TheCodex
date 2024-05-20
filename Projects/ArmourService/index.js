const PORT = 3001;
const express = require('express');
const app = express();
const armourController = require('./src/controller/armourController');  // replace with the actual path to your controller file

app.use(cors());
app.use(express.json());

// Use your router for requests to a certain path
app.use('/armour', armourController);  // replace '/armour' with the path you want

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});