describe('Notes App', () => {
    beforeEach(() => {
        cy.visit('https://localhost:44422')
    })

    it('should edit a note', () => {
        cy.get('.notes-list tbody tr:first-child td:nth-child(4) button:nth-child(1)')
            .should('be.visible')
            .click()

        cy.get('.notes-list tbody tr:first-child td:nth-child(1) input[type="text"]')
            .clear()
            .type('Updated Note Title')

        cy.get('.notes-list tbody tr:first-child td:nth-child(2) textarea')
            .clear()
            .type('Updated Note Content')

        cy.get('.notes-list tbody tr:first-child td:nth-child(4) button.btn-success')
            .click()

        // Get the updated note row
        cy.get('.notes-list tbody tr:first-child')
            .should('contain', 'Updated Note Title')
            .should('contain', 'Updated Note Content')
            .then(($row) => {
                // Get the timestamp from the row
                const timestamp = $row.find('td').eq(3).text()

                // Get the creation date from the note
                const creationDate = Cypress.$('.notes-list tbody tr:first-child td:nth-child(3)').text()

                // Format the timestamp and creation date in the desired format
                const date = new Date(timestamp)
                const formattedTimestamp = date.toLocaleString([], { month: '2-digit', day: '2-digit', year: 'numeric', hour: 'numeric', minute: '2-digit', hour12: true })
                const formattedCreationDate = new Date(creationDate).toLocaleString([], { month: '2-digit', day: '2-digit', year: 'numeric', hour: 'numeric', minute: '2-digit', hour12: true })

                // Verify that the formatted timestamp matches the formatted creation date
                expect(formattedTimestamp).to.equal(formattedCreationDate)
            })
    })
})
