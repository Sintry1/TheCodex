
const PORT = 3004;
const express = require('express');
const app = express();

app.use(cors());
app.use(express.json());

app.use ('/jewellery', jewelleryController);

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});