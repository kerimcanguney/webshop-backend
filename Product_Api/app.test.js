const request = require('supertest')
const app = request('./app.js')

describe("Product Routes", () =>{
    it('Get all products', () =>{
        return request(app)
        .get('/products')
        .expect('Content-Type', /json/)
        .expect(200)
        .then((resp) =>{
            expect(resp.body).toEqual(
                expect.arrayContaining([
                    expect.objectContaining({
                        name: expect.any(String)
                    })
                ])
            )
        })
    })

    it('Get product 404', () =>{
        return request(app).get('/product/1').expect(404)
    })

    it('Create product', () =>{
        return request(app).post('/products').send({
            name: "test",
            price: 10,
            description: "test",
            sizes: ["test"]
        }).expect(201)
    })
})
