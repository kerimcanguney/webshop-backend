const express = require('express')
var app = express()
const mongoose = require('mongoose')
const cors = require('cors')

require('dotenv/config')

const productRouter = require('./routes/products')
const port = 3001;
const corsOption = {
    origin: ['http://localhost:3000']
}
mongoose.connect(process.env.DB_CONNECTION, { useNewUrlParser: true})

const db = mongoose.connection
db.on('error', (error)=> console.error(error))
db.once('open', () => console.log("connected to db!"))

app.use(express.json())
app.use(cors(corsOption))
    
// enabling routes
app.use('/products',productRouter)

// server port
app.listen(port, () => console.log(`Server listening on port http://localhost:${port}`))
