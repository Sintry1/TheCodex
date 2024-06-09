const redis = require('redis');
const client = redis.createClient();

client.on('error', (err) => {
    console.error('Error connecting to Redis:', err);
});

const set = (key, value) => {
    client.set(key, JSON.stringify(value), redis.print);
};

const get = (key) => {
    return new Promise((resolve, reject) => {
        client.get(key, (err, data) => {
            if (err) {
                reject(err);
            } else {
                resolve(JSON.parse(data));
            }
        });
    });
};

module.exports = {
    set,
    get
};