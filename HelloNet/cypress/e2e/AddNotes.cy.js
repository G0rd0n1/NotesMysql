describe('Notes App', () => {
    beforeEach(() => {
        cy.visit('https://localhost:44422')
    })

    it('should add a new note', () => {
        cy.get('.input-section input[type="text"]').type('New Note Title')
        cy.get('.input-section textarea').type('New Note Content')
        cy.get('.input-section button[type="submit"]').click()
        cy.get('.notes-list tbody tr').should('have.length.gt', 0)
        cy.contains('.notes-list tbody tr', 'New Note Title')
        cy.contains('.notes-list tbody tr', 'New Note Content')
    })
})