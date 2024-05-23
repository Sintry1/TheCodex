const PORT = 3003;
const express = require('express');
const cors = require('cors');
const Sentry = require('@sentry/node');
const Tracing = require('@sentry/tracing');
const app = express();
const jewelleryController = require('./src/controller/jewelleryController');

Sentry.init({
    dsn: 'https://34d73dc0fc0fb04f1c44b8ec758fce46@o4506960048881664.ingest.us.sentry.io/4507298954346496',
    integrations: [
        new Sentry.Integrations.Http({ tracing: true }),
        new Tracing.Integrations.Express({ app: express }),
    ],
    tracesSampleRate: 1.0,
});

app.use(Sentry.Handlers.requestHandler());
app.use(Sentry.Handlers.tracingHandler());

app.use(cors());
app.use(express.json());

// Use your router for requests to a certain path
app.use('/jewellery', jewelleryController);  // replace '/jewellery' with the path you want

app.use(Sentry.Handlers.errorHandler());

app.use(function onError(err, req, res, next) {
  res.statusCode = 500;
  res.end(res.sentry + '\n');
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});