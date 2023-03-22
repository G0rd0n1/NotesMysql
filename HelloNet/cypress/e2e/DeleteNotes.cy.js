describe('Notes App', () => {
    let initialLength;

    beforeEach(() => {
        cy.visit('https://localhost:44422');

        // Get the initial length of the notes list
        cy.get('.notes-list tbody tr').then(($rows) => {
            initialLength = $rows.length;
        });
    });

    it('should delete an existing note', () => {
        // Click the first delete note button
        cy.get('.notes-list tbody tr:first-child td:nth-child(4) .btn-danger')
            .click();

        // Check that the number of rows in the notes list has decreased by one
        cy.get('.notes-list tbody tr').should('have.length', initialLength - 1);
    });
});
